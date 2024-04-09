using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    // [Header("NETWORK JOIN")]
    // [SerializeField] bool startGameAsClient;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGameAsClient()
    {
        //Shut down because have started as host during the title screen
        NetworkManager.Singleton.Shutdown();

        //Then restart as client
        NetworkManager.Singleton.StartClient();
    }
    // private void Update()
    // {
    //     if (startGameAsClient)
    //     {
    //         startGameAsClient = false;
    //
    //         //Shut down because have started as host during the title screen
    //         NetworkManager.Singleton.Shutdown();
    //
    //         //Then restart as client
    //         NetworkManager.Singleton.StartClient();
    //     }
    // }

}
