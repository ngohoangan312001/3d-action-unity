using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AN
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        [SerializeField] PlayerManager player;

        [Header("Save/Load")] 
        [SerializeField] private bool saveGame;
        [SerializeField] private bool loadGame;
        
        [Header("World Sence Index")]
        [SerializeField] int worldSceneIndex = 1;

        [Header("Current character data")] 
        public CharacterSlot currentCharacterSlot;

        public CharacterSaveData currentCharacterData;

        [Header("Save Data Writer")] 
        private DataWriter dataWriter;
        
        [Header("Character Slots")] 
        public CharacterSaveData characterSlot01;
        public CharacterSaveData characterSlot02;
        public CharacterSaveData characterSlot03;
        public CharacterSaveData characterSlot04;
        public CharacterSaveData characterSlot05;
        public CharacterSaveData characterSlot06;
        public CharacterSaveData characterSlot07;
        public CharacterSaveData characterSlot08;
        public CharacterSaveData characterSlot09;
        public CharacterSaveData characterSlot10;
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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadAllCharacterProfiles();
        }

        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }
            
            if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }

        public string GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot characterSlot)
        {
            string fileName = "";
            switch (characterSlot)
            {
                case CharacterSlot.CharacterSlot_01:
                    fileName = CharacterSlot.CharacterSlot_01.ToString();
                    break;
                case CharacterSlot.CharacterSlot_02:
                    fileName = CharacterSlot.CharacterSlot_02.ToString();
                    break;
                case CharacterSlot.CharacterSlot_03:
                    fileName = CharacterSlot.CharacterSlot_03.ToString();
                    break;
                case CharacterSlot.CharacterSlot_04:
                    fileName = CharacterSlot.CharacterSlot_04.ToString();
                    break;
                case CharacterSlot.CharacterSlot_05:
                    fileName = CharacterSlot.CharacterSlot_05.ToString();
                    break;
                case CharacterSlot.CharacterSlot_06:
                    fileName = CharacterSlot.CharacterSlot_06.ToString();
                    break;
                case CharacterSlot.CharacterSlot_07:
                    fileName = CharacterSlot.CharacterSlot_07.ToString();
                    break;
                case CharacterSlot.CharacterSlot_08:
                    fileName = CharacterSlot.CharacterSlot_08.ToString();
                    break;
                case CharacterSlot.CharacterSlot_09:
                    fileName = CharacterSlot.CharacterSlot_09.ToString();
                    break;
                case CharacterSlot.CharacterSlot_10:
                    fileName = CharacterSlot.CharacterSlot_10.ToString();
                    break;
                default:
                    break;
            }

            return fileName;
        }

        public void CreateNewGame()
        {
            //Create new file base on character slot
            GetCharacterFileNameBaseOnCharacterSlot(currentCharacterSlot);

            currentCharacterData = new CharacterSaveData();
        }
        
        //Load all character data on device
        private void LoadAllCharacterProfiles()
        {
            dataWriter = new DataWriter();

            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_01);
            characterSlot01 = dataWriter.LoadSaveFile();

            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_02);
            characterSlot02 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_03);
            characterSlot03 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_04);
            characterSlot04 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_05);
            characterSlot05 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_06);
            characterSlot06 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_07);
            characterSlot07 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_08);
            characterSlot08 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_09);
            characterSlot09 = dataWriter.LoadSaveFile();
            
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(CharacterSlot.CharacterSlot_10);
            characterSlot10 = dataWriter.LoadSaveFile();
        }
        
        public void LoadGame()
        {
            dataWriter = new DataWriter(); 
            
            //load file base on character slot
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(currentCharacterSlot);
            
            currentCharacterData = dataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldSence());
        }

        public void SaveGame()
        {
            dataWriter = new DataWriter();
            
            //Save file base on character slot
            dataWriter.saveFileName = GetCharacterFileNameBaseOnCharacterSlot(currentCharacterSlot);
            
            //Pass player info to save file
            player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);
            //Write player info to json file and save on local machine
            dataWriter.CreateNewSaveFile(currentCharacterData);

        }
        
        public IEnumerator LoadWorldSence()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }

        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}