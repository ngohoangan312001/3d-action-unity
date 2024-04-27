using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class WeaponManager : MonoBehaviour
    {
        public MeleeWeaponDamageCollider meleeWeaponDamageCollider;

        private void Awake()
        {
            meleeWeaponDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        }

        public void SetWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
        {
            meleeWeaponDamageCollider.characterCausingDamage = characterWieldingWeapon;
            meleeWeaponDamageCollider.physicalDamage = weapon.physicalDamage;
            meleeWeaponDamageCollider.magicDamage = weapon.magicDamage;
            meleeWeaponDamageCollider.pyroDamage = weapon.pyroDamage; 
            meleeWeaponDamageCollider.hydroDamage = weapon.hydroDamage;
            meleeWeaponDamageCollider.geoDamage = weapon.geoDamage;
            meleeWeaponDamageCollider.luminaDamage = weapon.luminaDamage;
            meleeWeaponDamageCollider.eclipeDamage = weapon.eclipeDamage;
            
            meleeWeaponDamageCollider.poiseDamage = weapon.poiseDamage;
            
            meleeWeaponDamageCollider.lightAttackModifier = weapon.lightAttackModifier;
            meleeWeaponDamageCollider.heavyAttackModifier = weapon.heavyAttackModifier;
            meleeWeaponDamageCollider.runningLightAttackModifier = weapon.runningLightAttackModifier;
            meleeWeaponDamageCollider.runningHeavyAttackModifier = weapon.runningHeavyAttackModifier;
        }
    }
}
