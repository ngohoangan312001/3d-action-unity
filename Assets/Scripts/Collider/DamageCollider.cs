using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class DamageCollider : MonoBehaviour
    {
        public WeaponItem currentWeapon;
        
        [Header("Attacking Charater")] 
        public CharacterManager characterCausingDamage; 
        
        [Header("Weapon Attack Modifier")] 
        public float lightAttackModifier;
        public float heavyAttackModifier;
        public float runningLightAttackModifier;
        public float runningHeavyAttackModifier;
        
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
            
        }

        protected virtual void DamageTarget(CharacterManager damageTarget)
        {
            //return if the target have been damaged by this
            //so the target will not be damaged more than once in a single attack
            if (characterDamaged.Contains(damageTarget)) return;
            
            characterDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
            PassDamageToDamageEffect(damageEffect);

            switch (characterCausingDamage.characterCombatManager.currentAttackType)
            {
                case AttackType.LightAttack:
                    ApplyAttackModifier(lightAttackModifier, damageEffect);
                    break;
                case AttackType.HeavyAttack:
                    ApplyAttackModifier(heavyAttackModifier, damageEffect);
                    break;
                case AttackType.RunningLightAttack:
                    ApplyAttackModifier(runningLightAttackModifier, damageEffect);
                    break;
                case AttackType.RunningHeavyAttack:
                    ApplyAttackModifier(runningHeavyAttackModifier, damageEffect);
                    break;
                default:
                    break;
            }
            
            //damageTarget.characterEffectManager.ProcessInstanceEffect(damageEffect);
            
            //FOR NETWORK
            if (characterCausingDamage.IsOwner)
            {
                damageTarget.characterNetworkManager.NotifyServerOfCharacterDamageServerRPC
                    (
                        damageTarget.NetworkObjectId,
                        characterCausingDamage.NetworkObjectId,
                        damageEffect.physicalDamage,
                        damageEffect.magicDamage,
                        damageEffect.pyroDamage,
                        damageEffect.hydroDamage,
                        damageEffect.geoDamage,
                        damageEffect.luminaDamage,
                        damageEffect.eclipseDamage,
                        damageEffect.poiseDamage,
                        damageEffect.angleHitFrom,
                        damageEffect.contactPoint.x,
                        damageEffect.contactPoint.y,
                        damageEffect.contactPoint.z
                    );
            }
            
        }

        protected virtual void ApplyAttackModifier(float modifier, TakeDamageEffect damageEffect)
        {
            damageEffect.physicalDamage *= modifier;
            damageEffect.magicDamage *= modifier;
            damageEffect.pyroDamage *= modifier; 
            damageEffect.hydroDamage *= modifier;
            damageEffect.geoDamage *= modifier;
            damageEffect.luminaDamage *= modifier;
            damageEffect.eclipseDamage *= modifier;

            damageEffect.poiseDamage *= modifier;
            
            //Todo: Apply full charge attack modifier after if attack is fully charge
        }
        
        protected virtual void PassDamageToDamageEffect(TakeDamageEffect damageEffect)
        {
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.pyroDamage = pyroDamage; 
            damageEffect.hydroDamage = hydroDamage;
            damageEffect.geoDamage = geoDamage;
            damageEffect.luminaDamage = luminaDamage;
            damageEffect.eclipseDamage = eclipeDamage;
            
            damageEffect.poiseDamage = poiseDamage;
            
            damageEffect.contactPoint = contactPoint;
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
