using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class Room
    {
        public const int MAXPLAYERS = 4;

        public GameTable Table { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public Room()
        {
            Name = Guid.NewGuid().ToString();
            Users = new List<User>();
            Table = new GameTable(6, 4);
        }

        public Room(string name)
        {
            Name = name;
            Users = new List<User>();
            Table = new GameTable(6, 4);
        }

        public void addUser(User user)
        {
            if (Users.Count <= Room.MAXPLAYERS)
            {
                Users.Add(user);
            }
            else throw new OverflowException("Too much users in " + Name + " room!");
        }

        public void start()
        {
            foreach (Cell c in Table.Table)
            {
                if (c.Col == 0 && c.Row == 0) c.Owner = Users[0];
                else if (c.Col == 0 && c.Row == 1) c.Owner = Users[0];
                else if (c.Col == 0 && c.Row == 4) c.Owner = Users[1];
                else if (c.Col == 0 && c.Row == 5) c.Owner = Users[1];
                else if (c.Col == 3 && c.Row == 0) c.Owner = Users[2];
                else if (c.Col == 3 && c.Row == 1) c.Owner = Users[2];

                else if (c.Col == 3 && c.Row == 4) c.Owner = Users[3];
                else if (c.Col == 3 && c.Row == 5) c.Owner = Users[3];
                else c.Owner = new User() { Color = new byte[3] { 100, 100, 100 } };


            }
        }
    }
}
