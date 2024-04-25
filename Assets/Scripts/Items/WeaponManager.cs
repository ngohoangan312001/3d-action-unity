using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private MeleeWeaponDamageCollider meleeWeaponDamageCollider;

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
        }
    }
}
