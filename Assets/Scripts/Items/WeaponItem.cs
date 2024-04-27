using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AN
{
    public class WeaponItem : Item
    {
        //Todo: ANIMATOR CONTROLLER OVERRIDE (Change attack animations base on current weapon

        [Header("Weapon Model")] public GameObject weaponModel;
        
        [Header("Weapon Requirement")] 
        public int levelREQ = 0;

        [Header("Weapon Guard Absorption")]
        public int guardAbsorption = 10;
        
        [Header("Weapon base Damage")]
        public int physicalDamage = 0; // Standard, Slash, Pierce, Crush
        public int magicDamage = 0;
        public int pyroDamage = 0; // Electro
        public int hydroDamage = 0; // Ice
        public int geoDamage = 0; 
        public int luminaDamage = 0; 
        public int eclipeDamage = 0;
        
        [Header("Weapon base poise Damage")]
        public int poiseDamage = 10;
        
        //WEAPON MODIFIERS
        [Header("Attack Modifier")]
        public float lightAttackModifier = 1.2f;
        public float heavyAttackModifier = 2f;
        public float runningLightAttackModifier = 1.2f;
        public float runningHeavyAttackModifier = 2f;
        
        //Light Attack
        //Heavy Attack
        //Critical Damage Modifier

        [Header("Stamina Cost Modifier")] 
        public int baseStaminaCost = 20;
        //Running Attack Stamina Cost Modifier
        public float runningLightAttackStaminaCostMultiplier = 1;
        public float runningHeavyAttackStaminaCostMultiplier = 1.5f;
        //Light Attack Stamina Cost Modifier
        public float lightAttackStaminaCostMultiplier = 0.8f;
        //Heavy Attack Stamina Cost Modifier
        public float heavyAttackStaminaCostMultiplier = 2;

        // Item Base Action
        [Header("Actions")] 
        public WeaponItemAction oh_Attack_Action;//One Hand Attack Action

        // Weapon Skill

        // Blocking sound fx

    }
}
