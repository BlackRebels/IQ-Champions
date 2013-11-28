using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreamDecorators
{
    class Program
    {
        static void Main(string[] args)
        {
           // runServer();
           // runClient("this is the dummy message");
            Coffee coffee = new SimpleCoffee();
            Console.WriteLine(coffee.ToString());
            Coffee coffeeSugar = new Sugar(coffee);
            Console.WriteLine(coffeeSugar.ToString());
            Coffee coffeePlusMilk = new Milk(coffeeSugar);
            Console.WriteLine(coffeePlusMilk.ToString());
            Console.ReadKey();
        }
        private static void runServer()
        {
            //ctrl+pötty   auto using
        TcpListener listener = new TcpListener(IPAddress.Any,8888);
        new Thread(() =>
        {
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream netStream = client.GetStream();
            using (CryptoStream cryptoStream = new CryptoStream(netStream, new SHA512Managed(), CryptoStreamMode.Read))
            {
                using (GZipStream zipStream = new GZipStream(cryptoStream, CompressionMode.Decompress)) 
                {
                    using (BufferedStream buffStream = new BufferedStream(zipStream, 64))
                    {
                        using (FileStream fileStream = new FileStream("message.txt", FileMode.Create))
                        {
                            int data = buffStream.ReadByte();
                            while (data != -1)
                            {
                                fileStream.WriteByte((byte)data);
                                data = buffStream.ReadByte();
                            }
                        }
                    }
                }
            }
        }).Start();
        Thread.Sleep(1000);
        }
        private static void runClient(string message)
        {
            TcpClient client = new TcpClient("localhost", 8888);
            NetworkStream netStream = client.GetStream();
            using (CryptoStream cryptoStream = new CryptoStream(netStream, new SHA512Managed(), CryptoStreamMode.Write))
            {
                using (GZipStream zipStream = new GZipStream(cryptoStream, CompressionMode.Compress))
                {
                    using (BufferedStream buffStream = new BufferedStream(zipStream, 64))
                    {
                        foreach (var b in Encoding.Unicode.GetBytes(message))
                        {
                            buffStream.WriteByte(b);
                        }
                    }
                }
            }
        }
    }
}
