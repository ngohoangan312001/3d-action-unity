using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
namespace AN
{
    public class TitleScreenManager : MonoBehaviour
    {
        [Header("Menus")]
        [SerializeField] private GameObject titleScreenMainMenu;
        [SerializeField] private GameObject titleScreenLoadMenu;
        
        [Header("Buttons")]
        [SerializeField] Button loadGameMenuReturnButton;
        [SerializeField] Button loadGameMenuOpenButton;
        public void StartNetworkAtHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.CreateNewGame();
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldSence());
        }

        public void OpenTheLoadGameMenu()
        {
            //Open load menu
            titleScreenLoadMenu.SetActive(true);
            //Close main menu
            titleScreenMainMenu.SetActive(false);
            
            //select return button
            loadGameMenuReturnButton.Select(); 
        }
        
        public void CloseTheLoadGameMenu()
        {
            //Close load menu
            titleScreenLoadMenu.SetActive(false);
            //Open main menu
            titleScreenMainMenu.SetActive(true);
            
            //select load game button
            loadGameMenuOpenButton.Select();
        }
    }

}
