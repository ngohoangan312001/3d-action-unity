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
        private RectTransform rectTransform;
        
        [Header("Bar Option")] 
        [SerializeField] protected bool scaleBarLengthWithStatus = true;
        [SerializeField] protected float widthScaleMultiplier = 1;
        
        //Todo: secondary bar for effect (yellow bar to slow lost value)

        protected void Awake()
        {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
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

            if (scaleBarLengthWithStatus)
            {
                rectTransform.sizeDelta = new Vector2(maxValue * widthScaleMultiplier, rectTransform.sizeDelta.y);
                
                //Reset position of the bar base on layout group setting
                PlayerUIManager.instance.playerUIHudManager.RefreshHUD();
            }
        }
    }
}
