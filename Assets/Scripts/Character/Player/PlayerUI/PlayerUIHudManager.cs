using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] private UI_StatBar healthBar;
        [SerializeField] private UI_StatBar staminaBar;
        [SerializeField] private UI_StatBar energyBar;
        
        
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
    }
}
