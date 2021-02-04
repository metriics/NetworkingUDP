using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPServer
{
    public static void RunServer()
    {
        byte[] buffer = new byte[512];

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ip = host.AddressList[1];

        Console.WriteLine($"Server name: {host.HostName} | IP: {ip}");

        IPEndPoint localEP = new IPEndPoint(ip, 11111);

        Socket server = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

        IPEndPoint client = new IPEndPoint(IPAddress.Any, 0); // 0 is for any available port
        EndPoint remoteClient = (EndPoint)client;

        try
        {
            server.Bind(localEP);
            Console.WriteLine("Waiting...");

            while (true)
            {
                int rec = server.ReceiveFrom(buffer, ref remoteClient);
                Console.WriteLine($"Recieved from {remoteClient.ToString()}: {Encoding.ASCII.GetString(buffer, 0, rec)}");
            }

            server.Shutdown(SocketShutdown.Both);
            server.Close();
        } catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        RunServer();
        Console.Read();
        return 0;
    }
}