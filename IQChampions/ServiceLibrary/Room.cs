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
        public static readonly byte[] defaultcolor = new byte[] { 225, 225, 225 };

        public GameTable Table { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        private List<User> rollable;
        private User actualPlayer = new User();
        private Cell actualCell = null;

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
                else c.Owner = new User() { Color = defaultcolor };
            }
            newTurn();
        }



        public void newTurn()
        {
            actualPlayer.State = States.IDLE;
            if (rollable == null || rollable.Count == 0)
            {
                rollable = new List<User>();
                rollable.AddRange(Users);
            }

            int r = IQService.rand.Next(rollable.Count);
            actualPlayer = rollable[r];
            rollable.RemoveAt(r);

            actualPlayer.State = States.MOVE;
        }

        public bool Move(int x, int y)
        {
            actualCell = Table.Table.Find(c => c.Row == x && c.Col == y);
            if (actualCell.Owner.Name == null)
            {
                // Üres mező, kérdést dob
                actualPlayer.State = States.ANSWER;
                return true;
            }
            else
            {
                actualCell = null;
                return false;
            }
        }

        public bool Answer(int id)
        {
            try
            {
                actualPlayer.State = States.IDLE;
                if (id == 0) // helyes
                {
                    actualCell.Owner = actualPlayer;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                newTurn();
            }
        }

    }
}
