using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary
{
    public class Room
    {
        public const int MAXPLAYERS = 4;

        private string name;
        private List<string> users;

        public List<string> Users
        {
            get { return users; }
        }

        public Room()
        {
            users = new List<string>();
            name = Guid.NewGuid().ToString();
        }

        public Room(string name)
        {
            users = new List<string>();
            this.name = name;
        }

        public void addUser(string user)
        {
            if (users.Count <= MAXPLAYERS)
            {
                users.Add(user);
            }
            else throw new OverflowException("Too much users in " + name + " room!");
        }
    }
}
