using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace AN
{
    public class CharacterAnimtorManager : MonoBehaviour
    {
        CharacterManager character;

        private float vertical;
        private float horizontal;
        
        
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            if (!character.canMove)
            {
                return;
            }
            
            if (character.characterNetworkManager.isSprinting.Value)
            {
                verticalValue = 2;
            }
            
            character.animator.SetFloat("horizontal",horizontalValue, 0.1f, Time.deltaTime);
            character.animator.SetFloat("vertical",verticalValue, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(
            string targetAction, 
            bool isPerformingAction, 
            bool applyRootMotion = true, 
            bool canRotate = false, 
            bool canMove = false)
        {
            //apply motion from the animation
            character.applyRootMotion = applyRootMotion;
            
            character.animator.CrossFade(targetAction, 0.2f);

            //Stop character form attempting new action
            //ex: turn flag to true when character get damaged and is performing damage animation
            //so we can check this flag before attempting a new action
            character.isPerformingAction = isPerformingAction;
            character.canRotate = canRotate;
            character.canMove = canMove;
            
            //tell server/host to play animation
            character.characterNetworkManager.NotifyServerOfActionAnimationServerRPC(NetworkManager.Singleton.LocalClientId, targetAction, applyRootMotion);
        }
        
        public virtual void PlayTargetAttackActionAnimation(string targetAction, 
            bool isPerformingAction, 
            bool applyRootMotion = true, 
            bool canRotate = false, 
            bool canMove = false)
        {
            //TODO: Check last attack performed ==> for combos
            //TODO: Check current attack type
            //TODO: Update animation set to current weapon animation
            //TODO: Check if weapon can be parried
            //TODO: Update isAttacking flag to network
            
            //apply motion from the animation
            character.applyRootMotion = applyRootMotion;
            
            character.animator.CrossFade(targetAction, 0.2f);

            //Stop character form attempting new action
            //ex: turn flag to true when character get damaged and is performing damage animation
            //so we can check this flag before attempting a new action
            character.isPerformingAction = isPerformingAction;
            character.canRotate = canRotate;
            character.canMove = canMove;
            
            //tell server/host to play animation
            character.characterNetworkManager.NotifyServerOfAttackActionAnimationServerRPC(NetworkManager.Singleton.LocalClientId, targetAction, applyRootMotion);
        }
    }
    
    
}
