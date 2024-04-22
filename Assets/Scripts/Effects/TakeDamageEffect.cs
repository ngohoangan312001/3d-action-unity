using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    [CreateAssetMenu(menuName = "Character Effects/Instance Effects/Take Damage")]
    public class TakeDamageEffect : InstanceCharacterEffect
    {
        [Header("Character Casusing Damage")] 
        //Damage cause by another character, the Character will be store here
        public CharacterManager characterCausingDamage;

        [Header("Damage")] 
        public float physicalDamage = 0; // Standard, Slash, Pierce, Crush
        public float magicDamage = 0;
        public float pyroDamage = 0; // Electro
        public float hydroDamage = 0; // Ice
        public float geoDamage = 0; 
        public float luminaDamage = 0; 
        public float eclipeDamage = 0;

        [Header("Final Damage")] 
        private int finalDamageDealt = 0;
        
        [Header("Poise")]
        public float poiseDamage = 0;
        public bool poiseIsBroken = false; //If character poise is broken, character will be stunned and play a damage animation
        
        //todo: Build up effects

        [Header("Animation")] 
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;
        
        [Header("Sound FX")] 
        public bool willPlayDamageSFX = true;
        public AudioClip elementalDamageSFX; //Used on top of regular sfx if there is elemental damage present

        [Header("Direction Damage taken from")]
        public float angleHitFrom; //Determine what damage animation to play (move backward, left, right,...)
        public float contactPoint; //Determine where the blood FX instantiate
        
        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);
            
            //No additional damage fx should be process if character is dead
            if (character.isDead.Value)
                return;

            CalculateDamage(character);

            //Todo: Check for "Invulnerablity"
            //Todo: Calculate Damage
            //Todo: Check direction damage came from
            //Todo: Play damage animation
            //Todo: Check for buildup 
            //Todo: Play damage SFX
            //Todo: Play damage Visual FX (VFX)
            //Todo: If character is A.I, check for new target if character causing damage is present

        }

        private void CalculateDamage(CharacterManager character)
        {
            if (!character.IsOwner) return;
            
            if (characterCausingDamage != null)
            {
                //Todo: check for damage modifiers and modify base damage ( damage buff,....)
            }

            //Todo: Check for character flat defenses and subtract it from damage

            //Todo: Check character absorption and subtract it from damage
            
            //Add all damage types together and apply final damage
            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + pyroDamage + hydroDamage + geoDamage +
                                                luminaDamage + eclipeDamage);
            
            //Attack will deal at least 1 damage
            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;
            
            //Todo: Calculate poise damage
        }
    }
}