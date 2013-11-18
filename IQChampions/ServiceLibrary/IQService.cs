using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQUtil;
using ServiceLibrary;

namespace IQChampionsServiceLibrary
{
    public class IQService : IIQService
    {
        private static List<User> onlineUsers = null;
        private static List<string> queue = null;
        private static List<Room> rooms = null;
        private const int timeout = 500000;
        private static Object lockObject = new Object();

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
                        o.isOnline = false;
                    }));
                }
                Thread.Sleep(timeout / 2);
                lock (lockObject)
                {
                    Parallel.ForEach(onlineUsers, new Action<User>((o) =>
                    {
                        if (!o.isOnline)
                        {
                            if (queue.Remove(o.getName))
                            {
                                Logger.log(Errorlevel.INFO, o.getName + " leaved queue");
                            }
                            onlineUsers.Remove(o);
                            Logger.log(Errorlevel.INFO, o.getName + " timed out");
                        }
                    }));
                }
            }
        }

        public bool Login(string user, string pass)
        {
            // debug, adminra belép
            if (onlineUsers.Exists(x => x.getName.Equals(user)))
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
                onlineUsers.RemoveAll(x => x.getName.Equals(user));
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
                    User userFromOnlines = onlineUsers.Find(x => x.getName.Equals(user));
                    if (userFromOnlines == null)
                    {
                        return false;
                    }
                    else
                    {
                        userFromOnlines.isOnline = true;
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

        #region Chat

        public void SendAll(string name, string message)
        {
            throw new NotImplementedException();
        }

        public List<string> getAllMesages()
        {
            throw new NotImplementedException();
        }

        public void SendPrivate(string from, string to, string message)
        {
            throw new NotImplementedException();
        }

        public List<string> getPrivateMesages(string from)
        {
            throw new NotImplementedException();
        }

        public void SendRoom(string name, string message, string szobanev)
        {
            throw new NotImplementedException();
        }

        public List<string> getRoomMesages(string szobanev)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUsers()
        {
            throw new NotImplementedException();
        }

        public string[] GetStatistic(string username)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatistic(string name, int kerdeszam, int helyesvalasz)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region APIkezelő

        //DA MIGHTY PING
        public APIenum APIping(string name, string roomname)
        {
            Random r = new Random();
            Boolean enum_sent = false;
            APIenum e = (APIenum)1;
            while (!enum_sent)
            {
                int random = r.Next(1, 32);
                if (random == 1
                    || random == 2
                    || random == 5
                    || random == 6
                    || random == 10
                    || random == 11
                    || random == 12
                    || random == 13
                    || random == 14
                    || random == 20
                    || random == 21
                    || random == 22
                    || random == 23
                    || random == 25
                    || random == 26
                    || random == 27
                    || random == 28
                    || random == 30
                    || random == 31
                    || random == 32)
                {
                    e = (APIenum)random;
                    enum_sent = true;
                }

            }
            if (e != APIenum.STANDBY
                && e != APIenum.GAME_STANDBY
                && e != APIenum.ROOMLIST_CHANGED
                && e != APIenum.USERLIST_CHANGED)
            {
                Console.WriteLine(name + "    " + e.ToString());
            }
            return e;
        }

        #region Szobakezelő

        //szoba létrehozása
        public bool createRoom(string name, string roomname, string[] beállítások)
        {
            throw new NotImplementedException();
        }

        //szobához csatlakozás
        public bool joinRoom(string name, string roomname)
        {
            throw new NotImplementedException();
        }

        //szoba visszaküldése, ha van változás, REFRESH ROOM, ha nincs, ROOM STANDBY, ha elindult, GAME STARTED
        public Room getMyRoom(string name)
        {
            throw new NotImplementedException();
        }

        //szobák nevének kilistázása
        public List<String> getRooms()
        {
            throw new NotImplementedException();
        }

        //szoba visszaküldése
        public Room getRoom(string roomname)
        {
            throw new NotImplementedException();
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
                        r.addUser(queue[0]);
                        queue.RemoveAt(0);
                    }

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

        //belső metódus-mivel tele van, egyből GAME STARTED
        public bool roomFound(string user)
        {
            Room room = rooms.Find(x => x.Users.Contains(user));
            return room != null;
        }

        #endregion

        #region JátékKezelő

        //játékosok becsatlakoztatása a játékba, Játéktér legenerálása, UI előkészítése
        public bool startGame(string name, string roomname)
        {
            throw new NotImplementedException();
        }

        // Pálya frissítése, Kérdések betöltése, Játékosok sorrendjének kialakítása
        public GameTable getGameTable(string name, string roomname)
        {
            throw new NotImplementedException();
        }

        //visszaadja a jelenlegi állást és a frissített táblát, a következő kérdést és a lépő játékost generálja, GAME ENDED ha vége a játéknak, vagy mindenki kilépett
        public GameTable refreshGameTable(string roomname)
        {
            throw new NotImplementedException();
        }

        //az aktuális játékos you can movet kap, a többiek waitet
        public string getActualPlayer(string roomname)
        {
            throw new NotImplementedException();
        }

        //visszamaradó idő lekérése
        public int getMoveRemainingTime(string roomname)
        {
            throw new NotImplementedException();
        }

        //játékos jelölése, kérdés hozzárendelése
        public bool chooseCell(int x, int y)
        {
            throw new NotImplementedException();
        }

        //visszaadja, hogy mennyi idő van még vissza
        public bool getIfMoved(string roomname)
        {
            throw new NotImplementedException();
        }
        //visszaadja a kérdést és az aktív játékosokat, akik válaszolhatnak
        public Question getQuestion(string roomname)
        {
            throw new NotImplementedException();
        }

        //visszamaradó idő lekérése
        public int getAnswerRemainingTime(string roomname)
        {
            throw new NotImplementedException();
        }

        //válsz bejelölése, ha mindSenki válaszolt, PLAYERS ANSWERED jön vissza, ha vannak még vissza WAITINGFORANSWER jön vissza
        public bool chooseAnswer(string name, string roomname, short sorszam)
        {
            throw new NotImplementedException();
        }

        //visszaadja ha valaki/mindenki válaszolt és a visszamaradó időt
        public bool getIfAnswered(string roomname)
        {
            throw new NotImplementedException();
        }

        //visszaadja a válaszokat és részidejüket, a helyes választ, frissíti a statisztikájukat
        public string[] getAnswerResults(string roomname)
        {
            throw new NotImplementedException();
        }

        //igény szerint visszaadja az end reditet, statisztikát, felugró ablakot hogy nertél, akármit, triggereli az ui változtatását
        public string[] getStatistics(string roomname)
        {
            throw new NotImplementedException();
        }


        //ha mindenki kilépett, a szoba törlődik, a játékoslista frissül a lobbyba került emberekkel, a játékból kikerült embereknek frissül a roomlist
        public bool returnToLobby(string roomname, bool lobby)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }

    public class Question
    {

    }


    public class GameTable
    {

    }
}
