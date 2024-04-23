using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AN
{
    public class PlayerManager : CharacterManager
    {
        [Header("Debug Menu")] 
        [SerializeField] private bool respawnCharacter = false;
        [SerializeField] private bool switchRightWeapon = false;
        
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatManager playerStatManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        protected override void Awake()
        {
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatManager = GetComponent<PlayerStatManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
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
            
            DebugMenu();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;
                WorldSaveGameManager.instance.player = this;
                
                // sử dụng += để đăng ký phương thức SetNewStamninaValue với OnValueChanged của NetworkVariable,
                // hai giá trị cũ và mới sẽ luôn được truyền vào phương thức đó khi sự kiện xảy ra.
                // do OnValueChanged là một sự kiện được thiết kế để thông báo về sự thay đổi của giá trị,
                // nó luôn luôn truyền vào hai giá trị: giá trị cũ và giá trị mới.
                // không cần phải chỉ định các giá trị này khi đăng ký phương thức với sự kiện, Unity sẽ tự động làm điều đó.
                
                //Update UI stat bar when value change
                playerNetworkManager.currentHealth.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;
                playerNetworkManager.currentEnergy.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewEnergyValue;
                playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStamninaValue;
                
                playerNetworkManager.currentStamina.OnValueChanged += playerStatManager.ResetStaminaRegenerationTimer;
                
                //Update Max value of resource when stat change
                playerNetworkManager.vitality.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;
                playerNetworkManager.intellect.OnValueChanged += playerNetworkManager.SetNewMaxEnergyValue;
                playerNetworkManager.endurance.OnValueChanged += playerNetworkManager.SetNewMaxStaminaValue;
            }
            
            playerNetworkManager.currentHealth.OnValueChanged += playerNetworkManager.CheckHP;
            
            //Load Weapon in weapon id change
            playerNetworkManager.currentRightHandWeaponId.OnValueChanged += playerNetworkManager.OnCurrentRightHandWeaponIDChange;
            playerNetworkManager.currentLeftHandWeaponId.OnValueChanged += playerNetworkManager.OnCurrentLeftHandWeaponIDChange;
        }

        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (IsOwner)
            {
                PlayerUIManager.instance.playerUIPopUpManager.OpenDeathPopUp();
            }
            
            return base.ProcessDeathEvent(manuallySelectDeathAnimation);

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

        //reference to current character data
        //that mean anything happen to character data in this 'SaveGameDataToCurrentCharacterData' or 'LoadGameDataFromCurrentCharacterData'
        //Will change the value of the currentCharacterData
        //but in this case, use reference to make sure that the currentCharacterData
        //is not any currentCharacterData but the current have been pass down this function
        public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.vitality = playerNetworkManager.vitality.Value;
            currentCharacterData.intellect = playerNetworkManager.intellect.Value;
            currentCharacterData.endurance = playerNetworkManager.endurance.Value;
            
            currentCharacterData.currentHealth = playerNetworkManager.currentHealth.Value;
            currentCharacterData.currentEnergy = playerNetworkManager.currentEnergy.Value;
            currentCharacterData.currentStamina = playerNetworkManager.currentStamina.Value;
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            Vector3 characterLoadedPosition = new Vector3(currentCharacterData.xPosition,currentCharacterData.yPosition,currentCharacterData.zPosition);
            transform.position = characterLoadedPosition;
            
            //Sync stat to network manager
            playerNetworkManager.vitality.Value = currentCharacterData.vitality ;
            playerNetworkManager.intellect.Value = currentCharacterData.intellect;
            playerNetworkManager.endurance.Value = currentCharacterData.endurance;
            
            //Set character healt/energy/stamina base on stat
            //Health
            int healthBaseOnStat = playerStatManager.CalculateHealthBaseOnStat(playerNetworkManager.vitality.Value);
            playerNetworkManager.maxHealth.Value = healthBaseOnStat;
            playerNetworkManager.currentHealth.Value = currentCharacterData.currentHealth;
            PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(playerNetworkManager.maxHealth.Value);
            //Energy
            int energyBaseOnStat = playerStatManager.CalculateEnergyBaseOnStat(playerNetworkManager.intellect.Value);
            playerNetworkManager.maxEnergy.Value = energyBaseOnStat;
            playerNetworkManager.currentEnergy.Value = currentCharacterData.currentEnergy;
            PlayerUIManager.instance.playerUIHudManager.SetMaxEnergyValue(playerNetworkManager.maxEnergy.Value);
            //Stamina
            int staminaBaseOnStat = playerStatManager.CalculateStaminaBaseOnStat(playerNetworkManager.endurance.Value);
            playerNetworkManager.maxStamina.Value = staminaBaseOnStat;
            playerNetworkManager.currentStamina.Value = currentCharacterData.currentStamina;
            PlayerUIManager.instance.playerUIHudManager.SetMaxStamninaValue(playerNetworkManager.maxStamina.Value);
        }

        public override void ReviveCharacter()
        {
            base.ReviveCharacter();
            
            if (IsOwner)
            {
                isDead.Value = false;
                playerNetworkManager.currentHealth.Value = playerNetworkManager.maxHealth.Value;
                playerNetworkManager.currentEnergy.Value = playerNetworkManager.maxEnergy.Value;
                playerNetworkManager.currentStamina.Value = playerNetworkManager.maxStamina.Value;
                
                //Play rebirth animation
                playerAnimatorManager.PlayTargetActionAnimation("Revive",false);
            }
        }

        // Will need to be delete later
        private void DebugMenu()
        {
            if (respawnCharacter)
            {
                respawnCharacter = false;
                ReviveCharacter();
            }
            if (switchRightWeapon)
            {
                switchRightWeapon = false;
                playerEquipmentManager.SwitchRightHandWeapon();
            }
        }
    }
}
