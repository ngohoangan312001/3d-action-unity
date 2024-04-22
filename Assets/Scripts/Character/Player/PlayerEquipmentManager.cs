using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        private PlayerManager player;
        public WeaponModelInstantiationSlot rightHandSlot;
        public WeaponModelInstantiationSlot leftHandSlot;

        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;
        
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
            
            InstantializeWeaponSlot();
        }

        protected override void Start()
        {
            loadWeaponOnBothHand();
        }

        public void InstantializeWeaponSlot()
        {
            WeaponModelInstantiationSlot[] weaponSlotList = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlotList)
            {
                    if (weaponSlot.weaponModelSlot == WeaponModelSlot.RightHand)
                    {
                        rightHandSlot = weaponSlot;
                    }
                    else if (weaponSlot.weaponModelSlot == WeaponModelSlot.LeftHand)
                    {
                        leftHandSlot = weaponSlot;
                    }
            }
        }

        public void loadWeaponOnBothHand()
        {
            loadWeaponOnRightHand();
            loadWeaponOnLeftHand();
        }
        
        public void loadWeaponOnRightHand()
        {
            if (player.playerInventoryManager.currentRightHandWeapon != null && rightHandSlot != null)
            {
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandSlot.LoadWeapon(rightHandWeaponModel);
            }
        }
        
        public void loadWeaponOnLeftHand()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon != null && leftHandSlot != null)
            {
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeapon(leftHandWeaponModel);
            }
        }
    }
}
