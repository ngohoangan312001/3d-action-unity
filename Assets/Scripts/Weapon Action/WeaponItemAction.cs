using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Test Actions")]
    public class WeaponItemAction : ScriptableObject
    {
        public int actionId;

        public virtual void AttempToPerformAction(PlayerManager playerPerformingAction,
            WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.IsOwner)
            {
                playerPerformingAction.playerNetworkManager.currentWeaponBeingUsedId.Value = weaponPerformingAction.itemId;
            }
            
            Debug.Log("Weapon Action Performed");
        }
    }
}