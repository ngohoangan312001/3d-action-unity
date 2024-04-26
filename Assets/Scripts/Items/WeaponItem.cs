using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Light Attack
        //Heavy Attack
        //Critical Damage Modifier

        [Header("Stamina Cost")] 
        public int baseStaminaCost = 20;
        //Running Attack Stamina Cost Modifier
        public int runAttackStaminaCost = 0;
        //Light Attack Stamina Cost Modifier
        public int lightAttackStaminaCost = 0;
        //Heavy Attack Stamina Cost Modifier
        public int heavyAttackStaminaCost = 0;

        // Item Base Action
        [Header("Actions")] 
        public WeaponItemAction oh_Attack_Action;//One Hand Attack Action

        // Weapon Skill

        // Blocking sound fx

    }
}
