using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;

        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;
        [SerializeField] float rotationSpeed = 15;

        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;

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
            // movement diraction base on camera's facing perspective and movement input
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;

            //Vector holds 2 pieces of information - a point in space and a magnitude.
            //The magnitude is the length of the line formed between(0, 0, 0) and the point in space.
            //If you "normalize" a vector(also known as the "unit vector"),
            //the result is a line that starts a(0, 0, 0) and "points" to your original point in space.
            moveDirection.Normalize();
            moveDirection.y = 0;

            if(moveAmount > 0.5f)
            {
                // Running
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (moveAmount <= 0.5f)
            {
                //Walking
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

        public void HandleRotation()
        {
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
        
    }
}
