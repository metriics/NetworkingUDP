using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// lecture 4
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPClient : MonoBehaviour
{
    [SerializeField] private GameObject myCube;

    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket client;

    public static void RunClient()
    {
        IPAddress ip = IPAddress.Parse("192.168.2.13");
        remoteEP = new IPEndPoint(ip, 11111);

        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        outBuffer = Encoding.ASCII.GetBytes("Test data :D");
        client.SendTo(outBuffer, remoteEP);

        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        RunClient();
    }
}
