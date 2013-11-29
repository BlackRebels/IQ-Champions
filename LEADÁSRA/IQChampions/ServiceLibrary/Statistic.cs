using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQChampionsServiceLibrary
{
    public class Statistic
    {
        public List<User> Users { get; set; }

        public Statistic()
        {
        }
        public Statistic(Room r)
        {
            Users = new List<User>(r.Players);
            Users.Sort(delegate(User user1, User user2)
            {
                return user2.Point.CompareTo(user1.Point);
            });
        }
    }
}
