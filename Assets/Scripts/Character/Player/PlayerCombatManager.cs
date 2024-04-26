using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace AN
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        private PlayerManager player;
        public WeaponItem currentWeaponBeingUsed;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBaseAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            if (player.IsOwner)
            {
                //Perform weapon action
                weaponAction.AttempToPerformAction(player,weaponPerformingAction);
            }
            
            //Notify the server to play animation
            player.playerNetworkManager.NotifyServerOfWeaponActionServerRPC(NetworkManager.Singleton.LocalClientId,weaponAction.actionId,weaponPerformingAction.itemId);
        }
    }
}
