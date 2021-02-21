using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MeleeMonsters/Monsters/Settings")]
public class MonsterScriptableObject : ScriptableObject
{
    public float speed = 10.0f;

    public float jumpStrength = 3.0f;
}
