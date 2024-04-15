using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class CharacterStatManager : MonoBehaviour
    {
        private CharacterManager character;
        
        [Header("Stamina Regeneration")] 
        [SerializeField] private float staminaRegenerationTimer = 0;
        //Tick Timer add small delay between each generation
        [SerializeField] private float staminaTickTimer = 0;
        [SerializeField] private float staminaRegenerationDelay = 2;
        [SerializeField] private float staminaRegenerationAmount = 2;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public int CalculateStaminaBaseOnEnduranceLevel(int endurance)
        {
            float stamina = 0;

            stamina = endurance * 3;

            return Mathf.RoundToInt(stamina);
        }
        
        public virtual void RegenerateStamina()
        {
            if (!character.IsOwner)
                return;

            if (character.characterNetworkManager.isSprinting.Value)
                return;

            if (character.isPerformingAction)
                return;

            staminaRegenerationTimer += Time.deltaTime;

            if (staminaRegenerationTimer >= staminaRegenerationDelay)
            {
                if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
                {
                    staminaTickTimer += Time.deltaTime;
                    if (staminaTickTimer >= 0.1)
                    {
                        staminaTickTimer = 0;
                        character.characterNetworkManager.currentStamina.Value += staminaRegenerationAmount;
                    }
                }
            }
        }

        public virtual void ResetStaminaRegenerationTimer(float previousStaminaAmount, float currentStaminaAmount)
        {
            //DO: Reset generation timer when a action used stamina
            //DON'T: Reset generation timer when stamina is generating

            if (currentStaminaAmount < previousStaminaAmount)
            {
                staminaRegenerationTimer = 0;
            }
        }
    }
}