using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AN
{
    public class RangeWeaponDamageCollider : DamageCollider
    {
        [Header("VFX")] [SerializeField] private GameObject RangeWeaponAttackVFX;
        
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
                if (currentWeapon is RangeWeaponItem RangeWeapon)
                {
                    if (RangeWeapon.CanFire())
                    {
                        DisableDamageCollider();
                        PlayWeaponAttackVFX(transform.position);
                        RangeWeapon.currentAmmo--;
                        RangeWeapon.timeSinceLastAttack = 0;

                        OnRangeAttack();
                        
                        if (Physics.Raycast(transform.position, PlayerCamera.instance.transform.forward,
                                out RaycastHit hitInfo, RangeWeapon.maxDistance))
                        {
                            CharacterManager damageTarget = hitInfo.collider?.GetComponentInParent<CharacterManager>();
                            
                            contactPoint = hitInfo.point;

                            Debug.Log("Hit: " + hitInfo.transform.name);
                            
                            //Todo: Check if the damageTarget can receive damage from this gameObject

                            //Todo: Check if target is blocking

                            //Todo: Check if target is invulnerable 

                            Debug.DrawLine(transform.position, hitInfo.point, Random.ColorHSV(), 10f);

                            if (damageTarget != null)
                                DamageTarget(damageTarget);
                        }
                    }
                }
            }
        }

        private void Update()
        {
            if (currentWeapon is RangeWeaponItem RangeWeapon)
            {
                if (RangeWeapon.timeSinceLastAttack <= (1f / (RangeWeapon.attackRate / 60f)))
                    RangeWeapon.timeSinceLastAttack += Time.deltaTime;
            }
        }

        private void OnRangeAttack()
        {
            
        }
        
        public void PlayWeaponAttackVFX(Vector3 startPoint)
        {
            if (RangeWeaponAttackVFX != null)
            {
                GameObject bloodSplatter = Instantiate(RangeWeaponAttackVFX, startPoint, Quaternion.identity);
            }
            
        }
    }
}
