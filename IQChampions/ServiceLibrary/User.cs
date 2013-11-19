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

        public User()
        {
            Color = new byte[3];
            IQService.rand.NextBytes(Color);
            Online = true;
            State = States.IDLE;
        }

        public User(String name)
        {
            Name = name;
            Color = new byte[3];
            IQService.rand.NextBytes(Color);

            //Szürkés színek kiszűrése
            int avg = (Color[0] + Color[1] + Color[2]) / 3;
            int dif = 20;
            while (Math.Abs(Color[0] - avg) < dif && Math.Abs(Color[1] - avg) < dif && Math.Abs(Color[2] - avg) < dif)
            {
                IQService.rand.NextBytes(Color);
            }

            Online = true;
        }

    }
}
