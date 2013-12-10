using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IQChampionsServiceLibrary
{
    [ServiceContract]
    public interface IIQService
    {

        #region Felhasználó
        //felhasználói cuccok
        [OperationContract]
        int PingPeriod();

        [OperationContract]
        bool Login(string user, string Password);

        [OperationContract]
        bool Logout(string user);

        [OperationContract]
        bool Ping(string user);

        [OperationContract]
        List<String> getUserList();

        [OperationContract]
        String[] getUserStats(string username);

        #endregion

        
        #region Chat
        [OperationContract]
        void Send(string user, string message);

        [OperationContract]
        List<Message> getMesages(string user);

        /*
        //közös chat
        [OperationContract]
        void SendAll(string user, string message); //közös chat

        [OperationContract]
        List<string> getAllMesages(); //ő az aki megkérdezi h van -e uj üzenet

        //privát chat
        [OperationContract]
        void SendPrivate(string from, string to, string message);

        [OperationContract]
        List<string> getPrivateMesages(string from);

        //szoba cuccok
        [OperationContract]
        void SendRoom(string user, string message, string szobanev);

        [OperationContract]
        List<string> getRoomMesages(string szobanev);
        
        #endregion
        
        #region Statisztika
        
        [OperationContract]
        List<string> GetUsers(); //visszaadja az online játékosok nevét

        [OperationContract]
        string[] GetStatistic(string useruser); //megadott user statisztikájának megtekintése

        [OperationContract]
        bool UpdateStatistic(string user, int kerdeszam, int helyesvalasz); //saját offline statisztika feltöltése
        */
        #endregion
                 
        #region SzobaKezelő
        /*
        [OperationContract]
        Boolean createRoom(string user, string roomuser, string[] beállítások);
            
        [OperationContract]
        Boolean joinRoom(string user, string roomuser);
               
        [OperationContract]
        Room getMyRoom(string user);

        [OperationContract]
        List<string> getRooms();
   
        [OperationContract]
        Room getRoom(string roomuser);
       */
        #endregion
        

        #region SorKezelő

        [OperationContract]
        void joinQueue(string user);

        [OperationContract]
        void leaveQueue(string user);

        [OperationContract]
        int getQueuePosition(string user);

        [OperationContract]
        bool haveRoom(string user);

        #endregion

        #region JátékkezelőKezelő
        
        [OperationContract]
        GameTable getGameTable(string user);

        [OperationContract]
        States getMyState(string user);

        [OperationContract]
        bool Move(string user, int col, int row);

        [OperationContract]
        Question getQuestion(string user);

        [OperationContract]
        int getTimeLeft(string user);

        [OperationContract]
        bool answerQuestion(string user, int id);

        [OperationContract]
        Statistic getStatistics(string user);
        
        #endregion

    }
}
