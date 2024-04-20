using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN;

public class InstanceCharacterEffect : MonoBehaviour
{
    [Header("effect ID")] public int instanceEffectID;

    protected virtual void ProcessEffect(CharacterManager characterManager);
}