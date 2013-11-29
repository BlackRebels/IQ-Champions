using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class Message
    {
        public DateTime Time { get; set; }
        public User Sender { get; set; }
        public string Msg { get; set; }

        public Message()
        {
        }
        public Message(DateTime time, User sender, string message)
        {
            Time = time;
            Sender = sender;
            Msg = message;
        }
    }
}
