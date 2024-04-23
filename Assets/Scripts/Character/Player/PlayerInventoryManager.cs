using System.Collections;
using System.Collections.Generic;
using AN;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    public WeaponItem currentRightHandWeapon;
    public WeaponItem currentLeftHandWeapon;

    [Header("Quick Slot")]
    public WeaponItem[] weaponInRightHandSlots = new WeaponItem[3];
    public int rightHandWeaponIndex = 0;
    public WeaponItem[] weaponInLeftHandSlots = new WeaponItem[3];
    public int leftHandWeaponIndex = 0;
}
