using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteServer();
        }

        static void ExecuteServer()
        {
           
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAdd = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAdd, 11111);

                Socket listener = new Socket(ipAdd.AddressFamily, SocketType.Stream,
                                            ProtocolType.Tcp);

                try
                {
                    listener.Bind(localEndPoint);

                    listener.Listen(10);
                    Console.WriteLine("Listen?");
                    
                    while (true)
                    {   
                        Console.WriteLine("Waiting For Connection...");
                        var disconnect = Console.ReadLine();
                        if(disconnect == "exit"){
                            break;
                        }
                        Socket clientSocket = listener.Accept();
                        byte[] bytes = new Byte[1024];
                        string data = null;
                        while (string.IsNullOrEmpty(data))
                        {
                            int numByte = clientSocket.Receive(bytes);
                            Console.WriteLine(numByte);
                            data += Encoding.ASCII.GetString(bytes, 0, numByte);
                        }
                        Console.WriteLine($"Text Received => {data}");
                        var msg = Console.ReadLine();
                        byte[] message = Encoding.ASCII.GetBytes(msg);
                        clientSocket.Send(message);
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }    
                    
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.ToString());
                }
            
            
        }
    }
}
