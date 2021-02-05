using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.Runtime.InteropServices;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text hostText;

    // used for window name change within script
    [DllImport("user32.dll", EntryPoint = "SetWindowText")]
    public static extern bool SetWindowText(System.IntPtr hwnd, System.String lpString);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern System.IntPtr FindWindow(System.String className, System.String windowName);

    // Start is called before the first frame update
    void Start()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        hostText.text = $"{host.HostName} | {host.AddressList[1]}";
    }

    public void Client()
    {
        SetWindowTitle("Client");
        SceneManager.LoadScene(1);
    }

    public void Server()
    {
        SetWindowTitle("Server");
        SceneManager.LoadScene(2);
    }

    private void SetWindowTitle(string name)
    {
        //Get the window handle.
        var windowPtr = FindWindow(null, "NetworkingUDP");
        //Set the title text using the window handle.
        SetWindowText(windowPtr, name);
    }
}
