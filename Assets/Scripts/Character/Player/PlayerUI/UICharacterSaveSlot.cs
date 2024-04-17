using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AN
{
    public class UICharacterSaveSlot : MonoBehaviour
    {
        private DataWriter dataWriter;

        [Header("Game Slot")] 
        public CharacterSlot characterSlot;
        public TextMeshProUGUI slotNumber;

        [Header("Character Info")] 
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timePlayed;
        public TextMeshProUGUI world;

        private void Awake()
        {
            dataWriter = new DataWriter();
        }

        private void OnEnable()
        {
            LoadSaveSlot();
        }

        private void LoadSaveSlot()
        {
            // foreach (CharacterSlot slot in (CharacterSlot[])Enum.GetValues(typeof(CharacterSlot)))
            // {
            //     if (characterSlot != slot) continue;
            //     
            //     getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot02);
            //     break;
            // }
            
                switch (characterSlot)
                {
                    case CharacterSlot.CharacterSlot_01:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot01);
                        break;
                    case CharacterSlot.CharacterSlot_02:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot02);
                        break;
                    case CharacterSlot.CharacterSlot_03:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot03);
                        break;
                    case CharacterSlot.CharacterSlot_04:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot04);
                        break;
                    case CharacterSlot.CharacterSlot_05:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot05);
                        break;
                    case CharacterSlot.CharacterSlot_06:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot06);
                        break;
                    case CharacterSlot.CharacterSlot_07:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot07);
                        break;
                    case CharacterSlot.CharacterSlot_08:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot08);
                        break;
                    case CharacterSlot.CharacterSlot_09:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot09);
                        break;
                    case CharacterSlot.CharacterSlot_10:
                        getCharacterInfoInSlot( WorldSaveGameManager.instance.characterSlot10);
                        break;
                    default:
                        break;
            }
        }
        
        private void getCharacterInfoInSlot( CharacterSaveData characterSaveData)
        {
            dataWriter.saveFileName =
                WorldSaveGameManager.instance.GetCharacterFileNameBaseOnCharacterSlot(characterSlot);

            if (dataWriter.CheckFileExists())
            {
                slotNumber.text = "" + CharacterSlot.CharacterSlot_01;
                characterName.text = characterSaveData.characterName;
                timePlayed.text = "" + characterSaveData.secondsPlayed;
                world.text = "" + characterSaveData.sceneIndex;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void LoadGameFromCharacterSlot()
        {
            WorldSaveGameManager.instance.currentCharacterSlot = characterSlot;
            WorldSaveGameManager.instance.LoadGame();
        }

        public void SelectCurrentSlot()
        {
            TitleScreenManager.instance.SelectCharacterSlot(characterSlot);
        }
    }
}
