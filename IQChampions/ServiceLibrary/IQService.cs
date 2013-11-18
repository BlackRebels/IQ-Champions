﻿using System;
using System.Collections.Generic;
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
        private static List<User> onlineUsers = null;
        private static List<string> queue = null;
        private static List<Room> rooms = null;
        private const int timeout = 500000;
        private static Object lockObject = new Object();
        public static Random rand = new Random();

        static IQService()
        {
            onlineUsers = new List<User>();
            queue = new List<string>();
            rooms = new List<Room>();

            Thread kick = new Thread(new ThreadStart(kickOffline));
            kick.IsBackground = true;
            kick.Start();

            Logger.log(Errorlevel.INFO, "Initialized successful!");
        }

        #region Felhasználó kezelő

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
            else if (user.Length < 6)
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
            return 1000;
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
                Room ret = rooms.Find(x => x.Users.Find(y => y.Name.Equals(user)) != null);
                return ret;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
