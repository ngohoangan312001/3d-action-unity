using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")] 
        [SerializeField] protected Collider damageCollider;
        
        [Header("Damage")] 
        public float physicalDamage = 0; // Standard, Slash, Pierce, Crush
        public float magicDamage = 0;
        public float pyroDamage = 0; // Electro
        public float hydroDamage = 0; // Ice
        public float geoDamage = 0;
        public float luminaDamage = 0;
        public float eclipeDamage = 0;
        public float poiseDamage = 0;

        [Header("Contact Point")] 
        protected Vector3 contactPoint;
        
        [Header("Characters Damaged")] 
        protected List<CharacterManager> characterDamaged = new List<CharacterManager>();

        protected virtual void Awake()
        {
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                
                //Todo: Check if the damageTarget can receive damage from this gameObject
                
                //Todo: Check if target is blocking
                
                //Todo: Check if target is invulnerable 

                DamageTarget(damageTarget);
            }
        }

        protected virtual void DamageTarget(CharacterManager damageTarget)
        {
            //return if the target have been damaged by this
            //so the target will not be damaged more than once in a single attack
            if (characterDamaged.Contains(damageTarget)) return;
            
            characterDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
            PassDamageToDamageEffect(damageEffect);
            
            damageTarget.characterEffectManager.ProcessInstanceEffect(damageEffect);
        }

        protected void PassDamageToDamageEffect(TakeDamageEffect damageEffect)
        {
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.pyroDamage = pyroDamage; 
            damageEffect.hydroDamage = hydroDamage;
            damageEffect.geoDamage = geoDamage;
            damageEffect.luminaDamage = luminaDamage;
            damageEffect.eclipseDamage = eclipeDamage;
            
            damageEffect.poiseDamage = poiseDamage;
        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        
        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            characterDamaged.Clear(); //Reset character that have been hit when reset collider, so it can be hit again
        }
    }
}
