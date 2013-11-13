using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class IQService : IIQService
    {
        private static List<User> onlineUsers = null;
        private const int kickPeriod = 10000;
        private const int switchPeriod = 1000;

        private void kickOffline()
        {
            while (true)
            {
                Parallel.ForEach(onlineUsers, new Action<User>((o) =>
                {
                    if (!o.isOnline)
                    {
                        onlineUsers.Remove(o);
                        Console.WriteLine(DateTime.Now.ToString() + " - User " + o.getName + " kicked off");
                    }
                }));
                Thread.Sleep(kickPeriod);
            }
        }
        private void switchOffline()
        {
            while (true)
            {
                Parallel.ForEach(onlineUsers, new Action<User>((o) =>
                {
                    o.isOnline = false;
                }));
                Thread.Sleep(switchPeriod);
            }
        }

        public IQService()
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
            else return false;
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
            onlineUsers.Find(x => x.getName.Equals(user)).isOnline = true;
            Console.WriteLine(DateTime.Now.ToString() + " - User " + user + " pinged in");
            return true;
        }

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

        public void Szobaletrehozas()
        {
            throw new NotImplementedException();
        }

        public bool Szobacsatlakozas(string usernev, string szobanev)
        {
            throw new NotImplementedException();
        }

        public void Szobakilepes(string username, string szobanev)
        {
            throw new NotImplementedException();
        }

        public void SzobaInditas(string szobanev)
        {
            throw new NotImplementedException();
        }

        public void SzobaTorles(string szobanev)
        {
            throw new NotImplementedException();
        }

        public void SzobaPing(string szobanev)
        {
            throw new NotImplementedException();
        }
    }
}
