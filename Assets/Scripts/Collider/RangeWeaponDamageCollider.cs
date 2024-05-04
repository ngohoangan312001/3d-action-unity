using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class RangeWeaponDamageCollider : DamageCollider
    {
        protected override void Awake()
        {
            base.Awake();

            if (damageCollider == null)
            {
                damageCollider = GetComponent<Collider>();
            }
            
            //Disable weapon damage collider at start
            DisableDamageCollider();
        }
        
        public void FireRangeAttack()
        {
            if (characterCausingDamage.IsOwner)
            {
                if(currentWeapon is RangeWeaponItem RangeWeapon)
                    
                if (RangeWeapon.CanFire() && Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, RangeWeapon.maxDistance))
                {
                    CharacterManager damageTarget = hitInfo.collider?.GetComponentInParent<CharacterManager>();
                    contactPoint = hitInfo.point;
                    
                    Debug.Log("Hit: "+ hitInfo.transform.name);

                    RangeWeapon.currentAmmo--;
                    RangeWeapon.timeSinceLastAttack = 0;

                    OnRangeAttack();
                    
                    //Todo: Check if the damageTarget can receive damage from this gameObject
                
                    //Todo: Check if target is blocking
                
                    //Todo: Check if target is invulnerable 

                    if(damageTarget != null)
                    DamageTarget(damageTarget);
                }
            }
        }

        private void Update()
        {
            if (currentWeapon is RangeWeaponItem RangeWeapon)
            {
                if (RangeWeapon.timeSinceLastAttack <= (1f / RangeWeapon.attackRate / 60f))
                    RangeWeapon.timeSinceLastAttack += Time.deltaTime;
            }
            
            Debug.DrawRay(transform.position, transform.forward);
        }

        private void OnRangeAttack()
        {
            
        }
    }
}
