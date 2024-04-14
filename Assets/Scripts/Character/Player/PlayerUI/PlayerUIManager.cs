using System.Collections;
using System.Collections.Generic;
using AN;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [HideInInspector] public PlayerUIHudManager playerUIHudManager;
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

        playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
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

}
