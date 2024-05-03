using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace AN
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [Header("Stat Bar")]
        [SerializeField] private UI_StatBar healthBar;
        [SerializeField] private UI_StatBar staminaBar;
        [SerializeField] private UI_StatBar energyBar;
        
        [Header("Quick Slot")]
        [SerializeField] private QuickSlotWeapon[] rightWeaponQuickSlot;
        [SerializeField] private QuickSlotWeapon[] leftWeaponQuickSlot;
        
        //Health Bar
        public void SetNewHealthValue(float oldValue, float newValue)
        {
            healthBar.SetStat(Mathf.RoundToInt(newValue));
        }
        
        public void SetMaxHealthValue(int maxValue)
        {
            healthBar.SetMaxStat(maxValue);
        }
        
        //Energy Bar
        public void SetNewEnergyValue(float oldValue, float newValue)
        {
            energyBar.SetStat(Mathf.RoundToInt(newValue));
        }
        
        public void SetMaxEnergyValue(int maxValue)
        {
            energyBar.SetMaxStat(maxValue);
        }
        
        //Stamina Bar
        public void SetNewStamninaValue(float oldValue, float newValue)
        {
            staminaBar.SetStat(Mathf.RoundToInt(newValue));
        }
        
        public void SetMaxStamninaValue(int maxValue)
        {
            staminaBar.SetMaxStat(maxValue);
        }


        public void RefreshHUD()
        {
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }
        
        public void StartCoolDownWeaponSwitch(float characterWeaponSwitchCooldownTime, bool isRightWeapon = true){
            QuickSlotWeapon[] quickSlotList = rightWeaponQuickSlot;
            if(!isRightWeapon)
            {
                quickSlotList = leftWeaponQuickSlot;
            }

            foreach (var slot in quickSlotList)
                {
                    slot.StartCooldown(characterWeaponSwitchCooldownTime);
                }
        }

        public bool CheckWeaponIsCooldown(bool isRightWeapon = true){
            QuickSlotWeapon[] quickSlotList = rightWeaponQuickSlot;
            if(!isRightWeapon)
            {
                quickSlotList = leftWeaponQuickSlot;
            }

            return quickSlotList[0].isCooldown;
        }

        public void SetRightWeaponQuickSlotIcon(int[] weaponIds, int activeSlotIndex = - 1)
        {
            for (int i = 0; i < weaponIds.Length; i++)
            {
                WeaponItem weapon = WorldItemDatabase.instance.GetWeaponByID(weaponIds[i]);
                if(weapon == null)
                {
                    Debug.Log("Main Hand Weapon Item Slot " + i + " Is Null!");
                    rightWeaponQuickSlot[i].itemIcon.enabled = false;
                    rightWeaponQuickSlot[i].itemIcon.sprite = null;
                    
                    continue;
                }
                
                if(weapon.itemIcon == null)
                {
                    Debug.Log("Main Hand Weapon Slot " + i + " Has No Icon!");
                    rightWeaponQuickSlot[i].itemIcon.enabled = false;
                    rightWeaponQuickSlot[i].itemIcon.sprite = null;
                    
                    continue;
                }

                if (activeSlotIndex == i)
                {
                    rightWeaponQuickSlot[i].isEquiping = true;
                    rightWeaponQuickSlot[i].backGroundImage.color = Color.green;
                }
                else
                {
                    rightWeaponQuickSlot[i].isEquiping = false;
                    rightWeaponQuickSlot[i].backGroundImage.color = Color.black;
                }
                
                rightWeaponQuickSlot[i].itemIcon.enabled = true;
                rightWeaponQuickSlot[i].itemIcon.sprite = weapon.itemIcon;
            }
        }
        
        public void SetLeftWeaponQuickSlotIcon(int[] weaponIds, int activeSlotIndex = -1)
        {
            for (int i = 0; i < weaponIds.Length; i++)
            {
                WeaponItem weapon = WorldItemDatabase.instance.GetWeaponByID(weaponIds[i]);
                if(weapon == null)
                {
                    Debug.Log("Off Hand Weapon Item Slot " + i + " Is Null!");
                    leftWeaponQuickSlot[i].itemIcon.enabled = false;
                    leftWeaponQuickSlot[i].itemIcon.sprite = null;
                    
                    continue;
                }
                
                if(weapon.itemIcon == null)
                {
                    Debug.Log("Off Hand Weapon Slot " + i + " Has No Icon!");
                    leftWeaponQuickSlot[i].itemIcon.enabled = false;
                    leftWeaponQuickSlot[i].itemIcon.sprite = null;
                    
                    continue;
                }
                
                if (activeSlotIndex == i)
                {
                    leftWeaponQuickSlot[i].isEquiping = true;
                    leftWeaponQuickSlot[i].backGroundImage.color = Color.green;
                }
                else
                {
                    leftWeaponQuickSlot[i].isEquiping = false;
                    leftWeaponQuickSlot[i].backGroundImage.color = Color.black;
                }
                
                leftWeaponQuickSlot[i].itemIcon.enabled = true;
                leftWeaponQuickSlot[i].itemIcon.sprite = weapon.itemIcon;
            }
        }
    }
}
