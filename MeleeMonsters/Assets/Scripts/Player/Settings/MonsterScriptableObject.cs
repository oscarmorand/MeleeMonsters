using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MeleeMonsters/Monsters/Settings")]
public class MonsterScriptableObject : ScriptableObject
{
    public float speed = 10.0f;

    public float jumpStrength = 3.0f;

    public int extraJump = 1;

    public float wallSlidingSpeed = 5;

    public float yWallForce = 5;

    public float gravityScale = 1;
}
