using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace AN
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager instance;
            
        [Header("Menus")]
        [SerializeField] private GameObject titleScreenMainMenu;
        [SerializeField] private GameObject titleScreenLoadMenu;
        
        [Header("Buttons")]
        [SerializeField] Button loadGameMenuReturnButton;
        [SerializeField] Button loadGameMenuOpenButton;
        [SerializeField] Button titleScreenMainMenuNewGameButton;
        
        [Header("Pop Ups")]
        [SerializeField] private GameObject noCharacterSlotPopup;
        [SerializeField] private Button noCharacterSlotPopupConfirmButton;
        
        private void Awake()
        {
            //Can only have 1 instance of WorldSaveGameManager
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void StartNetworkAtHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.AttemptToCreateNewGame();
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

        public void OpenNoEmptySlotPopUp()
        {
            noCharacterSlotPopup.SetActive(true);
            noCharacterSlotPopupConfirmButton.Select();
        }
        
        public void CloseNoEmptySlotPopUp()
        {
            noCharacterSlotPopup.SetActive(false);
            titleScreenMainMenuNewGameButton.Select();
        }
    }

}
