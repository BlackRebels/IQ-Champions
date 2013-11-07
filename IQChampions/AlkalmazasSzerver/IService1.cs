using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AlkalmazasSzerver
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
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
        void Send(string name, string message); //közös chat
        
        [OperationContract]
        List<string> getMesages(); //ő az aki megkérdezi h van -e uj üzenet

        //privát chat
        [OperationContract]
        void Send(string from, string to, string message);
        
        [OperationContract]
        List<string> getMesages(string from);

        //szoba cuccok
        [OperationContract]
        void Send(string name, string message, string szobanev);
       
        [OperationContract]
        List<string> getMesages(string szobanev);
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


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
