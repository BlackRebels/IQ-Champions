using ServiceLibrary;
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
        int PingPeriod();

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

        #region APIkezelő

        [OperationContract]
        APIenum APIping(String name, String roomname);

        #region SzobaKezelő
        [OperationContract]
        Boolean createRoom(String name, String roomname, String[] beállítások);
            
        [OperationContract]
        Boolean joinRoom(String name, String roomname);
               
        [OperationContract]
        Room getMyRoom(String name);

        [OperationContract]
        List<String> getRooms();
   
        [OperationContract]
        Room getRoom(String roomname);
       
        #endregion

        #region SorKezelő
       
        [OperationContract]
        Boolean joinQueue(String name);
       
        [OperationContract]
        String getQueuePosition(String name);
       
        [OperationContract]
        Boolean joinFoundRoom(String name, String roomname);

        #endregion

        #region JátékkezelőKezelő

        [OperationContract]
        Boolean startGame(String name, String roomname);
    
        [OperationContract]
        GameTable getGameTable(String name, String roomname);
       
        [OperationContract]
        GameTable refreshGameTable(String roomname);
          
        [OperationContract]
        String getActualPlayer(String roomname);
             
        [OperationContract]
        Int32 getMoveRemainingTime(String roomname);
              
        [OperationContract]
        Boolean chooseCell(int x, int y);
              
        [OperationContract]
        Boolean getIfMoved(String roomname);
               
        [OperationContract]
        Question getQuestion(String roomname);
                          
        [OperationContract]
        Int32 getAnswerRemainingTime(String roomname);
                                   
        [OperationContract]
        Boolean chooseAnswer(String name, String roomname, Int16 sorszam);
                                      
        [OperationContract]
        Boolean getIfAnswered(String roomname);
                                          
        [OperationContract]
        String[] getAnswerResults(String roomname);
                                               
        [OperationContract]
        String[] getStatistics(String roomname);
                                          
        [OperationContract]
        Boolean returnToLobby(String roomname, Boolean lobby);

        #endregion

        #endregion
    }

    [ServiceContract]
    [DataContract]
    public class Question
    {

    }

    [ServiceContract]
    [DataContract]
    public class Room
    {

    }

    [ServiceContract]
    [DataContract]
    public class GameTable
    {

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
