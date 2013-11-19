using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class Question
    {
        public string Questionn { get; set; }
        public string GoodAnswer { get; set; }
        public string BadAnswer1 { get; set; }
        public string BadAnswer2 { get; set; }
        public string BadAnswer3 { get; set; }

        public Question()
        {                
        }
    }
}
