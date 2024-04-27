using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Charater")] 
        public CharacterManager characterCausingDamage; 
        
        [Header("Weapon Attack Modifier")] 
        public float lightAttackModifier;
        public float heavyAttackModifier;
        public float runningLightAttackModifier;
        public float runningHeavyAttackModifier;

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

        protected override void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();
            
            if (damageTarget != null)
            {
                //Prevent weapon to damaged the character wielding it
                if (damageTarget == characterCausingDamage) return;
                
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                
                //Todo: Check if the damageTarget can receive damage from this gameObject
                
                //Todo: Check if target is blocking
                
                //Todo: Check if target is invulnerable 

                DamageTarget(damageTarget);
            }
            
        }

        protected override void DamageTarget(CharacterManager damageTarget)
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

        private void ApplyAttackModifier(float modifier, TakeDamageEffect damageEffect)
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
    }
}
