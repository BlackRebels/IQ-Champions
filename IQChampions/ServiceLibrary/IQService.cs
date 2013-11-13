using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace IQChampionsServiceLibrary
{
    public class IQService : IIQService
    {
        public bool Login(string user, string pass)
        {
            // debug, adminra belép
            if (user.Equals("admin")) return true;
            else return false;
        }

        public bool Logout(string Name)
        {
            throw new NotImplementedException();
        }

        public bool Ping(string Name)
        {
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
