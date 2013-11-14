using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
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
