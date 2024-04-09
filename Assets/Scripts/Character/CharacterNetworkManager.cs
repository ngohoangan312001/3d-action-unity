using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace AN
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        private CharacterManager character;
        
        [Header("Position")]
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 networkPositionVelocity;
        public float networkPositionSmoothTime = 0.1f;
        public float networkRotationSmoothTime = 0.1f;
        
        [Header("Animator")]
        public NetworkVariable<float> animaterHorizontalNetworkParameter = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> animaterVerticalNetworkParameter = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> networkMoveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        //Code that is run on the Server (Host) , called by a Client.
        [ServerRpc]
        public void NotifyServerOfActionAnimationServerRPC(ulong clientId, string animationId, bool applyRootMotion)
        {
            // This character is the host -> send it id and animation to all client for another client to see it animation
            // IsServer: This property returns true if the object is active on an active server. This is only true if the object has been spawned
            if (IsServer)
            {
                PlayActionAnimationForAllClientClientRPC(clientId, animationId, applyRootMotion);
            }
        }
        
        //Code that is run on the Client, called by a Server (Host).
        //When the server is noticed that there is an action animation being performed
        //It will call this procedure, in this case: this character.animation == the once character who noticed the server will be call
        [ClientRpc]
        private void PlayActionAnimationForAllClientClientRPC(ulong clientId, string animationId, bool applyRootMotion)
        {
            if (clientId != NetworkManager.Singleton.LocalClientId)
            {
                PerformActionAnimationFormServer(animationId, applyRootMotion);
            }
        }

        private void PerformActionAnimationFormServer( string animationId, bool applyRootMotion)
        {
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(animationId, 0.2f);
        }
    }
}
