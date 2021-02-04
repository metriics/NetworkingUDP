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

    public static void NetworkingInit()
    {
        IPAddress ip = IPAddress.Parse("192.168.2.13");
        remoteEP = new IPEndPoint(ip, 11111);

        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    public static void SendData(string data)
    {
        outBuffer = Encoding.ASCII.GetBytes(data);
        client.SendTo(outBuffer, remoteEP);
    }

    public static void NetworkingShutdown()
    {
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        NetworkingInit();
        SendData(myCube.transform.position.ToString());
        NetworkingShutdown();
    }
}
