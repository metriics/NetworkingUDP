using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// lecture 4
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPServer : MonoBehaviour
{
    [SerializeField] private GameObject myCube;

    private static byte[] inBuffer = new byte[512];
    private Socket server;
    private EndPoint remoteClient;

    void NetworkingInit()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ip = host.AddressList[1];

        Debug.Log($"Server name: {host.HostName} | IP: {ip}");

        IPEndPoint localEP = new IPEndPoint(ip, 11111);

        server = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

        IPEndPoint client = new IPEndPoint(IPAddress.Any, 0); // port 0 is any port
        remoteClient = (EndPoint)client;

        try
        {
            server.Bind(localEP);
            Debug.Log("Networking initialized");
        }
        catch (Exception e)
        {
            Debug.LogError("Couldn't bind local endpoint");
        }
    }

    string RecieveData()
    {
        int rec = server.ReceiveFrom(inBuffer, ref remoteClient);
        return Encoding.ASCII.GetString(inBuffer, 0, rec);
    }

    void NetworkingShutdown()
    {
        try
        {
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Debug.Log("Networking shutdown");
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NetworkingInit();
    }

    // Update is called once per frame
    void Update()
    {
        string recievedData = RecieveData();
        string[] positions = recievedData.Split(',');

        myCube.transform.position = new Vector3(float.Parse(positions[0]), float.Parse(positions[1]), float.Parse(positions[2]));
    }

    void OnDestroy()
    {
        NetworkingShutdown();
    }
}
