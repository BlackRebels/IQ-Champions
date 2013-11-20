using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQUtil;

namespace IQChampionsServiceLibrary
{
    public class IQService : IIQService
    {
        public static Random rand = new Random();
        public static IQChampionsEntities database = null;
        public static int Pingperiod { get { return pingperiod; } }
        public static int Timeout { get { return timeout; } }

        private static List<User> onlineUsers = null;
        private static List<string> queue = null;
        private static List<Room> rooms = null;

        private static Object lockObject = new Object();
        private static int pingperiod = 1000;
        private static int timeout = 10000;

        static IQService()
        {
            onlineUsers = new List<User>();
            queue = new List<string>();
            rooms = new List<Room>();
            database = new IQChampionsEntities();

            var data = new Dictionary<string, string>();
            try
            {
                foreach (var row in File.ReadAllLines(".\\properties.txt"))
                {
                    string[] split = row.Split('=');
                    try
                    {
                        data.Add(split[0], split[1]);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Logger.log(Errorlevel.ERROR, "Property format error, missing \"=\" " + row);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.log(Errorlevel.ERROR, ex.Message);
            }
            try
            {
                pingperiod = int.Parse(data["pingperiod"]);
                Logger.log(Errorlevel.INFO, "Picked up property pingperiod=" + pingperiod);
            }
            catch (Exception)
            {
                Logger.log(Errorlevel.ERROR, "Property error at pingperiod, using default value " + pingperiod);
            }
            try
            {
                timeout = int.Parse(data["timeout"]);
                Logger.log(Errorlevel.INFO, "Picked up property timeout=" + timeout);
            }
            catch (Exception)
            {
                Logger.log(Errorlevel.ERROR, "Property error at timeout, using default value " + timeout);
            }

            Thread kick = new Thread(new ThreadStart(kickOffline));
            kick.IsBackground = true;
            kick.Start();

            Thread roomWatcher = new Thread(new ThreadStart(closeRooms));
            roomWatcher.IsBackground = true;
            roomWatcher.Start();

            Logger.log(Errorlevel.INFO, "Initialized successful!");
        }

        #region Felhasználó kezelő

        private static void closeRooms()
        {
            while (true)
            {
                Thread.Sleep(pingperiod);
                Parallel.ForEach(rooms, new Action<Room>((r) =>
                {
                    if (r.Finished)
                    {
                        rooms.Remove(r);
                        Logger.log(Errorlevel.INFO, r.Name + " room finished!");
                    }
                }));
            }
        }

        private static void kickOffline()
        {
            while (true)
            {
                Thread.Sleep(timeout / 2);
                lock (lockObject)
                {
                    Parallel.ForEach(onlineUsers, new Action<User>((o) =>
                    {
                        o.Online = false;
                    }));
                }
                Thread.Sleep(timeout / 2);
                lock (lockObject)
                {
                    Parallel.ForEach(onlineUsers, new Action<User>((o) =>
                    {
                        if (!o.Online)
                        {
                            if (queue.Remove(o.Name))
                            {
                                Logger.log(Errorlevel.INFO, o.Name + " leaved queue");
                            }
                            onlineUsers.Remove(o);
                            Logger.log(Errorlevel.INFO, o.Name + " timed out");
                        }
                    }));
                }
            }
        }

        public bool Login(string user, string pass)
        {
            // debug, adminra belép
            if (onlineUsers.Exists(x => x.Name.Equals(user)))
            {
                Logger.log(Errorlevel.INFO, user + " tried to log in twice");
                return false;
            }
            else if (user.Length < 10)
            {
                User login = new User(user);
                onlineUsers.Add(login);

                Logger.log(Errorlevel.INFO, user + " logged in");
                return true;
            }
            else
            {
                Logger.log(Errorlevel.INFO, (String.IsNullOrEmpty(user) ? "NULL OR EMPTY" : user) + " failed to log in");
                return false;
            }
        }

        public bool Logout(string user)
        {
            try
            {
                if (queue.Remove(user))
                {
                    Logger.log(Errorlevel.INFO, user + " leaved queue");
                }
                onlineUsers.RemoveAll(x => x.Name.Equals(user));
                Logger.log(Errorlevel.INFO, "User " + user + " logged out");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int PingPeriod()
        {
            return pingperiod;
        }

        public bool Ping(string user)
        {
            try
            {
                lock (lockObject)
                {
                    User userFromOnlines = onlineUsers.Find(x => x.Name.Equals(user));
                    if (userFromOnlines == null)
                    {
                        return false;
                    }
                    else
                    {
                        userFromOnlines.Online = true;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region SorKezelő

        public void joinQueue(string user)
        {
            if (!queue.Contains(user))
            {
                queue.Add(user);
                Logger.log(Errorlevel.INFO, user + " joined queue");

                if (queue.Count >= Room.MAXPLAYERS)
                {
                    Room r = new Room();

                    for (int i = 0; i < Room.MAXPLAYERS; i++)
                    {
                        r.addUser(onlineUsers.Find(x => x.Name.Equals(queue[0])));
                        queue.RemoveAt(0);
                    }
                    r.start();

                    rooms.Add(r);
                    Logger.log(Errorlevel.INFO, r.Name + " room created!");
                }
            }
            else
            {
                Logger.log(Errorlevel.ERROR, user + "tried to join queue twice!");
            }
        }
        public void leaveQueue(string user)
        {
            queue.Remove(user);
            Logger.log(Errorlevel.INFO, user + " leaved queue");
        }

        // viszamaradó idő válasza?
        public int getQueuePosition(string user)
        {
            if (queue.Count < Room.MAXPLAYERS)
            {
                return queue.Count - Room.MAXPLAYERS;
            }
            else
            {
                return queue.FindIndex(x => x == user);
            }
        }

        public bool haveRoom(string user)
        {
            return getRoomByUserName(user) != null;
        }

        #endregion

        #region Játék kezelő
        public GameTable getGameTable(string user)
        {
            try
            {
                return getRoomByUserName(user).Table;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        private Room getRoomByUserName(string user)
        {
            try
            {
                Room ret = rooms.Find(x => x.Players.Find(y => y.Name.Equals(user)) != null);
                return ret;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        private User getUserByUserName(string user)
        {
            return onlineUsers.Find(x => x.Name.Equals(user));
        }

        public States getMyState(string user)
        {
            return getUserByUserName(user).State;
        }

        public bool Move(string user, int x, int y)
        {
            try
            {
                return getRoomByUserName(user).Move(x, y);
            }
            catch (Exception ex)
            {
                Logger.log(Errorlevel.ERROR, ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }

        public Question getQuestion(string user)
        {
            return getRoomByUserName(user).Question;
        }

        public bool answerQuestion(string user, int id)
        {
            return getRoomByUserName(user).Answer(getUserByUserName(user), id);
        }

        public Statistic getStatistics(string user)
        {
            return new Statistic(getRoomByUserName(user));
        }
        #endregion

        #region Chat
        public void Send(string user, string message)
        {
            getRoomByUserName(user).Chat.Add(new Message(DateTime.Now,getUserByUserName(user), message));
        }
        public List<Message> getMesages(string user)
        {
            return getRoomByUserName(user).Chat;
        }
        #endregion

    }
}
