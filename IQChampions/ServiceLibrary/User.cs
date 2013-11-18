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

        public User()
        {
            Color = new byte[3];
            IQService.rand.NextBytes(Color);
            Online = true;
        }

        public User(String name)
        {
            Name = name;
            Color = new byte[3];
            IQService.rand.NextBytes(Color);
            Online = true;
        }

    }
}
