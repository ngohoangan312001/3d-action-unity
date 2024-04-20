using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class CharacterEffectManager : MonoBehaviour
    {
        private CharacterManager character;

        private void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        //Instance Effect (take damage, heal,...)
        public virtual void ProcessInstanceEffect(InstanceCharacterEffect instanceCharacterEffect)
        {
            instanceCharacterEffect.ProcessEffect(character);
        }
        
        //Timed Effect (debuff, buff,...)
        public virtual void ProcessTimedEffect()
        {
            
        }
        
        //Static Effect (equipment stats,... => non expired)
        public virtual void ProcessStaticEffect()
        {
            
        }
    }  
}
