using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class User
    {
        public string Name { get; set; }
        public byte[] Color { get; set; }
        public bool Online { get; set; }
        public States State { get; set; }
        public int Point { get; set; }
        public AnswerResult AnswerResult { get; set; }

        public User()
        {
            init();
        }

        public User(String name)
        {
            Name = name;
            init();
        }
        private void init()
        {
            Color = new byte[3];
            Online = true;
            State = States.IDLE;
            Point = 0;
            AnswerResult = null;
        }
    }
}
