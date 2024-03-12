using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class CharacterAnimtorManager : MonoBehaviour
    {
        CharacterManager character;

        private float vertical;
        private float horizontal;
        
        
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            character.animator.SetFloat("horizontal",horizontalValue, 0.1f, Time.deltaTime);
            character.animator.SetFloat("vertical",verticalValue, 0.1f, Time.deltaTime);
        }
    }
    
}
