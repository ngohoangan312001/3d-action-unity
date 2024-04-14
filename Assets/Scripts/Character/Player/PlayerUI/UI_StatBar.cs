using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace AN
{
    public class UI_StatBar : MonoBehaviour
    {
        private Slider slider;

        protected void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public virtual void SetStat(float newValue)
        {
            slider.value = newValue;
        }
        
        public virtual void SetMaxStat(int maxValue)
        {
            slider.maxValue = maxValue;
            
            //Max value change, set current value to max
            slider.value = maxValue;
        }
    }
}
