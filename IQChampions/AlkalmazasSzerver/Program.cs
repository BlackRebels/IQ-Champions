using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IQChampionsServiceLibrary;

namespace AlkalmazasSzerver
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(IQService)))
            {                
                host.Open();
                Console.WriteLine("Server is waiting for connections...");
                Console.ReadLine();
            }
        }
    }
}
