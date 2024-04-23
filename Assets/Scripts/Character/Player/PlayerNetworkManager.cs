using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

namespace AN
{
    public class PlayerNetworkManager : CharacterNetworkManager
    {
        private PlayerManager player;
        public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Unknown", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        
        [Header("Equipments")]
        public NetworkVariable<int> currentRightHandWeaponId = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentLeftHandWeaponId = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public void SetNewMaxHealthValue(int oldValue, int newValue)
        {
            maxHealth.Value = player.playerStatManager.CalculateHealthBaseOnStat(newValue);
            PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(maxHealth.Value);
            currentHealth.Value = maxHealth.Value;
        }
        public void SetNewMaxEnergyValue(int oldValue, int newValue)
        {
            maxEnergy.Value = player.playerStatManager.CalculateEnergyBaseOnStat(newValue);
            PlayerUIManager.instance.playerUIHudManager.SetMaxEnergyValue(maxEnergy.Value);
            currentEnergy.Value = maxEnergy.Value;
        }
        public void SetNewMaxStaminaValue(int oldValue, int newValue)
        {
            maxStamina.Value = player.playerStatManager.CalculateStaminaBaseOnStat(newValue);
            PlayerUIManager.instance.playerUIHudManager.SetMaxStamninaValue(maxStamina.Value);
            currentStamina.Value = maxStamina.Value;
        }
        
        public void OnCurrentRightHandWeaponIDChange(int oldID, int newID)
        {
            WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
            player.playerInventoryManager.currentRightHandWeapon = newWeapon;
            player.playerEquipmentManager.loadWeaponOnRightHand();
        }
        
        public void OnCurrentLeftHandWeaponIDChange(int oldID, int newID)
        {
            WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
            player.playerInventoryManager.currentLeftHandWeapon = newWeapon;
            player.playerEquipmentManager.loadWeaponOnLeftHand();
        }
    }
}