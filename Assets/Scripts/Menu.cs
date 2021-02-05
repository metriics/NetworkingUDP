using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text hostText;

    // Start is called before the first frame update
    void Start()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        hostText.text = $"{host.HostName} | {host.AddressList[1]}";
    }

    public void Client()
    {
        SceneManager.LoadScene(1);
    }

    public void Server()
    {
        SceneManager.LoadScene(2);
    }
}
