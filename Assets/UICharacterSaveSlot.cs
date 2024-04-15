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

        [Header("Game Slot")] public CharacterSlot characterSlot;

        [Header("Character Info")] 
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timePlayed;

        private void OnEnable()
        {
            LoadSaveSlot();
        }

        private void LoadSaveSlot()
        {
            dataWriter = new DataWriter();
                switch (characterSlot)
                {
                    case CharacterSlot.CharacterSlot_01:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot01);
                        break;
                    case CharacterSlot.CharacterSlot_02:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot02);
                        break;
                    case CharacterSlot.CharacterSlot_03:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot03);
                        break;
                    case CharacterSlot.CharacterSlot_04:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot04);
                        break;
                    case CharacterSlot.CharacterSlot_05:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot05);
                        break;
                    case CharacterSlot.CharacterSlot_06:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot06);
                        break;
                    case CharacterSlot.CharacterSlot_07:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot07);
                        break;
                    case CharacterSlot.CharacterSlot_08:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot08);
                        break;
                    case CharacterSlot.CharacterSlot_09:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot09);
                        break;
                    case CharacterSlot.CharacterSlot_10:
                        getCharacterInfoBySlot( WorldSaveGameManager.instance.characterSlot10);
                        break;
                    default:
                        break;
            }
        }
    

    private void getCharacterInfoBySlot( CharacterSaveData characterSaveData)
        {
            dataWriter.saveFileName =
                WorldSaveGameManager.instance.GetCharacterFileNameBaseOnCharacterSlot(characterSlot);

            if (dataWriter.CheckFileExists())
            {
                characterName.text = characterSaveData.characterName;
                timePlayed.text = ""+characterSaveData.secondsPlayed;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
