using System;
using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;

namespace AN
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Damage")] 
        public float physicalDamage = 0; // Standard, Slash, Pierce, Crush
        public float magicDamage = 0;
        public float pyroDamage = 0; // Electro
        public float hydroDamage = 0; // Ice
        public float geoDamage = 0;
        public float luminaDamage = 0;
        public float eclipeDamage = 0;

        [Header("Contact Point")] 
        protected Vector3 contactPoint;
        
        [Header("Characters Damaged")] 
        protected List<CharacterManager> characterDamaged = new List<CharacterManager>();
        private void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponent<CharacterManager>();

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
            if (characterDamaged.Contains(damageTarget)) return;
            
            characterDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
            PassDamageToDamageEffect(damageEffect);
            
            damageTarget.characterEffectManager.ProcessInstanceEffect(damageEffect);
        }

        private void PassDamageToDamageEffect(TakeDamageEffect damageEffect)
        {
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.pyroDamage = pyroDamage; 
            damageEffect.hydroDamage = hydroDamage;
            damageEffect.geoDamage = geoDamage;
            damageEffect.luminaDamage = luminaDamage;
            damageEffect.eclipeDamage = eclipeDamage;
        }
    }
}
