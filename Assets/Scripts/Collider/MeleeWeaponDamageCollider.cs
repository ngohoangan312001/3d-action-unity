using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Charater")] 
        public CharacterManager characterCausingDamage; 
    }
}
