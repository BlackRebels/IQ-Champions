using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceLibrary;

namespace IQChampionsServiceLibrary
{
    public class IQService : IIQService
    {
        private static List<User> onlineUsers = null;
        private const int kickPeriod = 10000;
        private const int switchPeriod = 1000;
        private static Object lockObject = new Object();

        #region Felhasználó kezelő

        private static void kickOffline()
        {
            while (true)
            {
                lock (lockObject)
                {
                    Parallel.ForEach(onlineUsers, new Action<User>((o) =>
                    {
                        if (!o.isOnline)
                        {
                            onlineUsers.Remove(o);
                            Console.WriteLine(DateTime.Now.ToString() + " - User " + o.getName + " kicked off");
                        }
                    }));
                }
                Thread.Sleep(kickPeriod);
            }
        }
        private static void switchOffline()
        {
            while (true)
            {
                lock (lockObject)
                {
                    Parallel.ForEach(onlineUsers, new Action<User>((o) =>
                    {
                        o.isOnline = false;
                    }));
                }
                Thread.Sleep(switchPeriod);
            }
        }

        static IQService()
        {

            onlineUsers = new List<User>();

            Thread kick = new Thread(new ThreadStart(kickOffline));
            Thread sw = new Thread(new ThreadStart(switchOffline));

            kick.IsBackground = true;
            sw.IsBackground = true;

            kick.Start();
            sw.Start();
            Console.WriteLine("Server initialized successful!");
        }

           
        public bool Login(string user, string pass)
        {
            // debug, adminra belép
            if (user.Equals("admin"))
            {
                User login = new User(user);
                onlineUsers.Add(login);

                Console.WriteLine(DateTime.Now.ToString() + " - User " + user + " logged in");
                return true;
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString() + " - User " + user + " failed to log in");
                return false;
            }
        }

        public bool Logout(string name)
        {
            try
            {
                onlineUsers.RemoveAll(x => x.getName.Equals(name));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Ping(string user)
        {
            try
            {
                lock (lockObject)
                {
                    onlineUsers.Find(x => x.getName.Equals(user)).isOnline = true;
                }
                Console.WriteLine(DateTime.Now.ToString() + " - User " + user + " pinged in");
                return true;
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

        //Sorhoz csatlakozás, a metódust ide kell megírni
        public bool joinQueue(string name)
        {
            throw new NotImplementedException();
        }
        //pozíció, vagy viszamaradó idő válasza, ha talált, szobanév visszaadása
        public string getQueuePosition(string name)
        {
            throw new NotImplementedException();
        }

        //belső metódus-mivel tele van, egyből GAME STARTED
        public bool joinFoundRoom(string name, string roomname)
        {
            throw new NotImplementedException();
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
}
