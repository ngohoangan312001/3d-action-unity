using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AN
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        private CharacterManager character;
        
        [Header("Ground Check & Jumping")] 
        //Force which character is jumping or falling
        [SerializeField] protected Vector3 yVelocity;
        //Force which is sticking character to the ground while grounded
        [SerializeField] protected float groundedYVelocity = -20;
        //Force which character begin to fall (will rise with the falling time)
        [SerializeField] protected float startFallingYVelocity = -5;
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTime = 0;
        [SerializeField] protected float gravityForce = -40;
        [SerializeField] protected float groundCheckPhereRadius = 0.1f;
        [SerializeField] protected LayerMask groundLayerMask = 0;
        
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        
        protected virtual void Update()
        {
            HandleGroundCheck();
        }

        protected void HandleGroundCheck()
        {
            character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckPhereRadius, groundLayerMask);
            character.characterNetworkManager.isJumping.Value = !character.isGrounded;
            
            if (character.isGrounded)
            {
                //Not Attemp to jump or move forward
                if (yVelocity.y < 0)
                {
                    inAirTime = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                //Character not on ground and not jumping => falling
                //and if character is falling and fallingVelocityHasBeenSet 
                //==> set update fall velocity and fall speed to start fall speed
                if (!character.characterNetworkManager.isJumping.Value && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = startFallingYVelocity;
                }

                inAirTime += Time.deltaTime;
                yVelocity.y += gravityForce * Time.deltaTime;
                
                character.animator.SetFloat("inAirTimer", inAirTime);
            }
            
            //Alway apply gravity to character
            character.characterController.Move(yVelocity * Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(character.transform.position,groundCheckPhereRadius);
        }
    }
}
