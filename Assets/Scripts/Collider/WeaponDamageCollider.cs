using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class WeaponDamageCollider : DamageCollider
    {
        public WeaponItem currentWeapon;
        
        [Header("Attacking Charater")] 
        public CharacterManager characterCausingDamage; 
        
        [Header("Weapon Attack Modifier")] 
        public float lightAttackModifier;
        public float heavyAttackModifier;
        public float runningLightAttackModifier;
        public float runningHeavyAttackModifier;

        protected override void DamageTarget(CharacterManager damageTarget)
        {
            //return if the target have been damaged by this
            //so the target will not be damaged more than once in a single attack
            if (characterDamaged.Contains(damageTarget)) return;
            
            characterDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
                
            PassDamageToDamageEffect(damageEffect);
            damageEffect.angleHitFrom = Vector3.SignedAngle(characterCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);
            
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
    }
}
