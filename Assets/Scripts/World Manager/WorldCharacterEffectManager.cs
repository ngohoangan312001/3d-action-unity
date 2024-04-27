using System;
using System.Collections;
using System.Collections.Generic;
using AN;
using UnityEngine;

namespace AN
{
    public class WorldCharacterEffectManager : MonoBehaviour
    {
        public static WorldCharacterEffectManager instance;

        [Header("Damage")] 
        public TakeDamageEffect takeDamageEffect;
        
        [SerializeField] private List<InstanceCharacterEffect> instanceCharacterEffects;
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

            GenerateEffectIDs();
        }

        private void GenerateEffectIDs()
        {
            for (int i = 0; i < instanceCharacterEffects.Count; i++)
            {
                instanceCharacterEffects[i].instanceEffectID = i;
            }
        }
    }
}
