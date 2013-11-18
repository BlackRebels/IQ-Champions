using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public enum APIenum
    {
        STANDBY             = 1,
        QUEUE_STANDBY       = 2,

        ROOMLIST_CHANGED    = 5,
        USERLIST_CHANGED    = 6,

        ROOM_STANDBY        = 10,
        ROOM_CREATED        = 11,
        ROOM_JOINED         = 12,
        ROOM_REFRESH        = 13,
        ROOM_FOUND          = 14,

        GAME_STANDBY        = 20,
        GAME_STARTED        = 21,
        GAME_REFRESH        = 22,
        GAME_ENDED          = 23,

        PLAYER_CAN_MOVE     = 25,
        YOU_CAN_MOVE        = 26,
        WAITING_FOR_MOVE    = 27,
        PLAYER_MOVED        = 28,

        YOU_CAN_ANSWER      = 30,
        WAITING_FOR_ANSWER  = 31,
        PLAYERS_ANSWERED    = 32,
        
    }
}
