using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class WorldUtilityManager : MonoBehaviour
    {
        public static WorldUtilityManager instance;

        [Header("Layers")]
        [SerializeField] private LayerMask characterLayers;
        [SerializeField] private LayerMask enviromentLayers;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public LayerMask GetCharacterLayers()
        {
            return characterLayers;
        }
        
        public LayerMask GetEnviromentLayers()
        {
            return enviromentLayers;
        }
    }
}
