using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteClient();
        }

        static void ExecuteClient()
        {
            try
            {
                //Retriving the hostname of my pc i.e DESKTOP- .....
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                //setting the ip address from the addresslist
                //the first item in the address list is the ipv6 address
                IPAddress ipAdd = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAdd, 11111);

                //Create TCP/IP Socket using Socket class constructor 
                //Socket sender = new Socket(ipAdd.AddressFamily, 
                //            SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    //Connect socket to local endpoint
                    
                    while (true)
                    {
                        //Create TCP/IP Socket using Socket class constructor 
                        Socket sender = new Socket(ipAdd.AddressFamily, 
                            SocketType.Stream, ProtocolType.Tcp);
                        sender.Connect(localEndPoint);
                        //Print the Endpoint information
                        Console.WriteLine($"Socket Connected to => {sender.RemoteEndPoint.ToString()}");
                        var msg = Console.ReadLine();
                        if(msg == "exit")
                            break;
                        //Creating a message to t=send to the server
                        byte[] messageSent = Encoding.ASCII.GetBytes(msg);
                        int byteSent = sender.Send(messageSent);

                        //data buffer
                        byte[] messageReceived = new byte[1024];

                        //We receive the message using the method Receive()
                        //this method returns number of bytes recieved that we will use to convert them to string
                        int byteReceiv = sender.Receive(messageReceived);
                        Console.WriteLine($"Message from server => {Encoding.ASCII.GetString(messageReceived, 0, byteReceiv)}");
                        
                        //Close The Socket
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }
                    

                    //Close The Socket
                    //sender.Shutdown(SocketShutdown.Both);
                    //sender.Close();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"ArgumentNullException : {e.ToString()}");
                }
                catch (SocketException e)
                {
                    Console.WriteLine($"SocketException : {e.ToString()}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"UnexpectedException : {e.ToString()}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            

            
        }
    }
}
