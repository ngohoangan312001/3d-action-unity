using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        [HideInInspector]
        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;

        [Header("Movement Setting")]
        [SerializeField] float walkingSpeed = 1;
        [SerializeField] float runningSpeed = 5;
        [SerializeField] float sprintingSpeed = 10;
        [SerializeField] float rollingSpeed = 5;
        [SerializeField] float rollTime = 0;
        [SerializeField] float rollDuration = 1;
        [SerializeField] float rotationSpeed = 15;
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;

        [Header("Roll")] 
        private Vector3 rollDirection;
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();
            if (player.IsOwner)
            {
                player.characterNetworkManager.animaterVerticalNetworkParameter.Value = verticalMovement;
                player.characterNetworkManager.animaterVerticalNetworkParameter.Value = horizontalMovement;
                player.characterNetworkManager.networkMoveAmount.Value = moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManager.animaterVerticalNetworkParameter.Value;
                horizontalMovement = player.characterNetworkManager.animaterVerticalNetworkParameter.Value;
                moveAmount = player.characterNetworkManager.networkMoveAmount.Value;
                
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
                
            }
        }

        public void HandleAllMovement()
        {
            GetMovementValues();
            
            //Ground
            HandleGroundedMovement();
            HandleRotation();
            //Aerial
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;
        }
        
        public void HandleGroundedMovement()
        {
            float moveSpeed = 0;
            if (!player.canMove)
                return;
            
            // movement diraction base on camera's facing perspective and movement input
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;

            //Vector holds 2 pieces of information - a point in space and a magnitude.
            //The magnitude is the length of the line formed between(0, 0, 0) and the point in space.
            //If you "normalize" a vector(also known as the "unit vector"),
            //the result is a line that starts a(0, 0, 0) and "points" to your original point in space.
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (player.PlayerNetworkManager.isSprinting.Value)
            {
                moveSpeed = sprintingSpeed;
            }
            else
            {
                if(moveAmount > 0.5f)
                {
                    // Running
                    moveSpeed = runningSpeed;
                }
                else if (moveAmount <= 0.5f)
                {
                    //Walking
                    moveSpeed = walkingSpeed;
                }
            }
            
            player.characterController.Move(moveDirection * (moveSpeed * Time.deltaTime));
            
            
        }

        public void HandleRotation()
        {
            if (!player.canRotate)
                return;
            
            targetRotationDirection = Vector3.zero;

            if (PlayerInputManager.instance.aimInput)
            {
                verticalMovement = 1;
            }
            
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            

            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;
            
            if(targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            
            transform.rotation = targetRotation;
        }

        public void AttemptToPerformDodge()
        {
            if(player.isPerformingAction)
                return;
            
            if (moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.transform.forward * verticalMovement;
                rollDirection += PlayerCamera.instance.transform.right * horizontalMovement;
                rollDirection.Normalize();
                rollDirection.y = 0;
                
                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;
                
                //Roll animation
                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward",true,true);
            }
            
            //if not moving => perform a backstep
            else
            {
                //Backstep animation
                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Backward",true,true);
            }
            
        }

        public void HandleSprinting()
        {
            if (player.isPerformingAction)
            {
                player.PlayerNetworkManager.isSprinting.Value = false;
                return;
            }
            
        //Out of stamina = false

        //Not moving = false
        //Moving = true
        if (moveAmount >= 0.5)
            {
                player.PlayerNetworkManager.isSprinting.Value = true;
            }
            else
            {
                player.PlayerNetworkManager.isSprinting.Value = false; 
            }
        }
    }
}
