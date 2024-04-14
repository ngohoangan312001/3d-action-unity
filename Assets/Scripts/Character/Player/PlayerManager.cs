using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AN
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatManager playerStatManager;
        protected override void Awake()
        {
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatManager = GetComponent<PlayerStatManager>();
        }

        protected override void Update()
        {
            base.Update();

            //if not the owner of this game object, return 
            if (!IsOwner)
            {
                return;
            }

            //Handle movement
            playerLocomotionManager.HandleAllMovement();
            
            //Stamina Regen
            playerStatManager.RegenerateStamina();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;
                
                // sử dụng += để đăng ký phương thức SetNewStamninaValue với OnValueChanged của NetworkVariable,
                // hai giá trị cũ và mới sẽ luôn được truyền vào phương thức đó khi sự kiện xảy ra.
                // do OnValueChanged là một sự kiện được thiết kế để thông báo về sự thay đổi của giá trị,
                // nó luôn luôn truyền vào hai giá trị: giá trị cũ và giá trị mới.
                // không cần phải chỉ định các giá trị này khi đăng ký phương thức với sự kiện, Unity sẽ tự động làm điều đó.
                playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStamninaValue;
                playerNetworkManager.currentStamina.OnValueChanged += playerStatManager.ResetStaminaRegenerationTimer;

                //TODO: Move this code when save and loading script added
                int staminaBaseOnEndurence = playerStatManager.CalculateStaminaBaseOnEnduranceLevel(playerNetworkManager.endurance.Value);
                playerNetworkManager.maxStamina.Value = staminaBaseOnEndurence;
                playerNetworkManager.currentStamina.Value = staminaBaseOnEndurence;
                PlayerUIManager.instance.playerUIHudManager.SetMaxStamninaValue(playerNetworkManager.maxStamina.Value);
            }
        }

        protected override void LateUpdate()
        {
            if (!IsOwner)
            {
                return;
            }
            base.LateUpdate();
            
            PlayerCamera.instance.HandleAllCameraActions();
        }
    }

}
