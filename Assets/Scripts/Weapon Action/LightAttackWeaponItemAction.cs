using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] private string light_Attack = "Main_Light_Attack";
        public override void AttempToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttempToPerformAction(playerPerformingAction, weaponPerformingAction);
            
            //If not the owner return, not perform the animation and the modifier 
            //==> only oner will need to do that
            if (!playerPerformingAction.IsOwner) return; 
            
            //Check To Stop Action
            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0) 
                return;
            
            if (!playerPerformingAction.isGrounded) 
                return;

            PerformLightAttackAction(playerPerformingAction, weaponPerformingAction);
        }
        
        public void PerformLightAttackAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isUsingRightHand.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack,light_Attack,true);
                return;
            }
            
            if (playerPerformingAction.playerNetworkManager.isUsingRightHand.Value)
            {
                return;
            }
        }
    }
}
