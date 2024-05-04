using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace AN
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        private PlayerManager player;
        public WeaponItem currentWeaponBeingUsed;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBaseAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            if (player.IsOwner)
            {
                //Perform weapon action
                weaponAction.AttempToPerformAction(player,weaponPerformingAction);
            }
            
            //Notify the server to play animation
            player.playerNetworkManager.NotifyServerOfWeaponActionServerRPC(NetworkManager.Singleton.LocalClientId, weaponAction.actionId,weaponPerformingAction.itemId);
        }

        public void DrainStaminaBaseOnAttack()
        {
            if (!player.IsOwner)
                return;

            if (currentWeaponBeingUsed == null) 
                return;
            
            float staminaDeducted = 0;

            switch (currentAttackType)
            {
                case AttackType.LightAttack:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost *
                                      currentWeaponBeingUsed.lightAttackStaminaCostMultiplier;
                    break;
                case AttackType.HeavyAttack:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost *
                                      currentWeaponBeingUsed.heavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.RunningLightAttack:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost *
                                      currentWeaponBeingUsed.runningLightAttackStaminaCostMultiplier;
                    break;
                case AttackType.RunningHeavyAttack:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost *
                                      currentWeaponBeingUsed.runningHeavyAttackStaminaCostMultiplier;
                    break;
                default:
                    break;
            }

            player.playerNetworkManager.currentStamina.Value -= Mathf.RoundToInt(staminaDeducted);
        }
    }
}
