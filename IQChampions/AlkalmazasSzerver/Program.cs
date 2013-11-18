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
        private static ServiceHost host = null;
        static void Main(string[] args)
        {
            try
            {
                host = new ServiceHost(typeof(IQService));

                host.Open();
                Logger.log(Errorlevel.INFO, "Waiting for connections...");
                Console.ReadKey();
                host.Close();
            }
            catch (Exception ex)
            {
                Logger.log(Errorlevel.ERROR, ex.Message + "\r\n" + ex.StackTrace);
                host.Close();
                Console.ReadKey();
            }
        }
    }
}
