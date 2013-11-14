using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IQChampionsServiceLibrary;
using IQUtil;

namespace AlkalmazasSzerver
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(IQService)))
            {
                host.Open();
                Logger.log(Errorlevel.INFO, "Waiting for connections...");
                Console.ReadLine();
            }
        }
    }
}
