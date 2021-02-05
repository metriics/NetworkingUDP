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
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ip = host.AddressList[1];

        remoteEP = new IPEndPoint(ip, 11111);

        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        Debug.Log("Networking initialized");
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
        Debug.Log("Networking shutdown");
    }

    // Start is called before the first frame update
    void Start()
    {
        NetworkingInit();
    }

    private void Update()
    {
        string data = $"{myCube.transform.position.x},{myCube.transform.position.y},{myCube.transform.position.z}";

        SendData(data);
    }

    private void OnDestroy()
    {
        NetworkingShutdown();
    }
}
