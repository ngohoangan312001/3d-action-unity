using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class CharacterCombatManager : MonoBehaviour
    {
        [Header("Current Target Lock On")]
        public CharacterManager currentTarget;
        
        [Header("Lock On Transform")]
        public Transform lockOnTransform;
        
        [Header("Current Attack Type")]
        public AttackType currentAttackType; 
        
        protected virtual void Awake()
        {
        }
    }
}
