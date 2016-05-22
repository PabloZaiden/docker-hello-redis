using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        
        private static async Task MainAsync(string[] args) {
            try {
                var server = "redisdb";
                
                IPAddress ip = null;
                try {
                    ip = (await Dns.GetHostAddressesAsync(server)).FirstOrDefault(dir => dir.AddressFamily == AddressFamily.InterNetwork);
                } catch (SocketException) {
                    System.Console.Error.WriteLine("Could not resolve " + server);
                    return;
                }          
                
                var redis = ConnectionMultiplexer.Connect(ip.ToString());
                var redisDB = redis.GetDatabase();
                
                while (true) {
                    try {
                        Thread.Sleep(1000);
                        System.Console.WriteLine("Linked to redis. Ping: " + redisDB.Ping());
                    } catch (Exception ex) {
                        System.Console.WriteLine(ex.ToString());
                    }
                }
            } catch (Exception ex) {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
