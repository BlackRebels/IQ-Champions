using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IQChampionsServiceLibrary
{
    [ServiceContract(Namespace = "http://iqchampions.com")]
    public interface IIQService
    {

        #region Felhasználó
        //felhasználói cuccok
        [OperationContract]
        bool Login(string Name, string Password);

        [OperationContract]
        bool Logout(string Name);

        [OperationContract]
        bool Ping(string Name);
        #endregion

        #region Chat
        //közös chat
        [OperationContract]
        void SendAll(string name, string message); //közös chat

        [OperationContract]
        List<string> getAllMesages(); //ő az aki megkérdezi h van -e uj üzenet

        //privát chat
        [OperationContract]
        void SendPrivate(string from, string to, string message);

        [OperationContract]
        List<string> getPrivateMesages(string from);

        //szoba cuccok
        [OperationContract]
        void SendRoom(string name, string message, string szobanev);

        [OperationContract]
        List<string> getRoomMesages(string szobanev);
        #endregion

        #region Statisztika
        [OperationContract]
        List<string> GetUsers(); //visszaadja az online játékosok nevét

        [OperationContract]
        string[] GetStatistic(string username); //megadott user statisztikájának megtekintése

        [OperationContract]
        bool UpdateStatistic(string name, int kerdeszam, int helyesvalasz); //saját offline statisztika feltöltése

        #endregion

        #region Szobakezelés
        [OperationContract]
        void Szobaletrehozas();

        [OperationContract]
        bool Szobacsatlakozas(string usernev, string szobanev);

        [OperationContract]
        void Szobakilepes(string username, string szobanev);

        [OperationContract]
        void SzobaInditas(string szobanev);

        [OperationContract]
        void SzobaTorles(string szobanev); //ha mégse akar játszani

        [OperationContract]
        void SzobaPing(string szobanev); //vmi bonyolult dolgot kell visszaadni NEM void
        #endregion

        #region Játékkezelő

        #endregion
    }

    [ServiceContract]
    [DataContract]
    public class User
    {
        private string name = null;
        private bool isonline = false;



        public User(String name)
        {
            this.name = name;
            isonline = true;
        }

        [DataMember]
        public string getName
        {
            get { return name; }
        }
        public bool isOnline
        {
            get { return isonline; }
            set { isonline = value; }
        }
    }
}
