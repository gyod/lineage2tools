using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace L2Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("L2Proxy :: Test\n");

            IPEndPoint loginListeningEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2107);
            IPEndPoint loginDestinationEndPoint = new IPEndPoint(IPAddress.Parse("192.168.100.150"), 2106);
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            Proxy proxy = new Proxy(loginListeningEndPoint, loginDestinationEndPoint, ip);

            Console.Out.WriteLine("Enter für Ende.");
            Console.In.ReadLine();

            proxy.StopLogin();
            proxy.StopGame();

        }
    }
}
