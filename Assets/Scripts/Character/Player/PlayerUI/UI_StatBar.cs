using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] protected float maxWidthScale = 800;

        [Header("Bar Option")]
        [SerializeField] private TextMeshProUGUI  currentValueText;
        [SerializeField] private TextMeshProUGUI maxValueText;
        //Todo: secondary bar for effect (yellow bar to slow lost value)

        protected void Awake()
        {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
        }

        public virtual void SetStat(float newValue)
        {
            slider.value = newValue;
            if(currentValueText != null && maxValueText != null) 
                currentValueText.text = newValue.ToString();
        }
        
        public virtual void SetMaxStat(int maxValue)
        {
            slider.maxValue = maxValue;

            if (currentValueText != null && maxValueText != null)
            {
                maxValueText.text = maxValue.ToString();
                currentValueText.text = maxValue.ToString();
            }
            
            //Max value change, set current value to max
            slider.value = maxValue;

            if (scaleBarLengthWithStatus)
            {
                float maxWidth = maxWidthScale > maxValue * widthScaleMultiplier ? (maxValue * widthScaleMultiplier) : maxWidthScale;
                rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.sizeDelta.y);
                
                //Reset position of the bar base on layout group setting
                PlayerUIManager.instance.playerUIHudManager.RefreshHUD();
            }
        }
    }
}
