using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AN
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponDamageCollider weaponDamageCollider;

        private void Awake()
        {
            weaponDamageCollider = GetComponentInChildren<WeaponDamageCollider>();
        }

        public void SetWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
        {
            weaponDamageCollider.characterCausingDamage = characterWieldingWeapon;
            weaponDamageCollider.physicalDamage = weapon.physicalDamage;
            weaponDamageCollider.magicDamage = weapon.magicDamage;
            weaponDamageCollider.pyroDamage = weapon.pyroDamage; 
            weaponDamageCollider.hydroDamage = weapon.hydroDamage;
            weaponDamageCollider.geoDamage = weapon.geoDamage;
            weaponDamageCollider.luminaDamage = weapon.luminaDamage;
            weaponDamageCollider.eclipeDamage = weapon.eclipeDamage;
            
            weaponDamageCollider.poiseDamage = weapon.poiseDamage;
            
            weaponDamageCollider.lightAttackModifier = weapon.lightAttackModifier;
            weaponDamageCollider.heavyAttackModifier = weapon.heavyAttackModifier;
            weaponDamageCollider.runningLightAttackModifier = weapon.runningLightAttackModifier;
            weaponDamageCollider.runningHeavyAttackModifier = weapon.runningHeavyAttackModifier;

            weaponDamageCollider.currentWeapon = weapon;
        }
    }
}
