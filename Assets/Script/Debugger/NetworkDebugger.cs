using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkDebugger : MonoBehaviour
{
    [SerializeField] private Button hostButton; 
    [SerializeField] private Button serverButton; 
    [SerializeField] private Button clientButton;


    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
            ActivateHost()
        );
        serverButton.onClick.AddListener(() =>
            ActivateServer()
        );
        clientButton.onClick.AddListener(() =>
            ActivateClient()
        );
    }

    void ActivateHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    void ActivateServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    void ActivateClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
