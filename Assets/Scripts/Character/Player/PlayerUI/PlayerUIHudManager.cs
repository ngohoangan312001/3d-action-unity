using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] private UI_StatBar staminaBar;

        public void SetNewStamninaValue(float oldValue, float newValue)
        {
            staminaBar.SetStat(Mathf.RoundToInt(newValue));
        }
        
        public void SetMaxStamninaValue(int maxValue)
        {
            staminaBar.SetMaxStat(maxValue);
        }
    }
}
