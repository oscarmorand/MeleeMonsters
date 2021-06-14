using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string name;
    public int damage;
    public float ejection;
    public Vector2 direction;
    public GameObject hitbox;
    public float time;
    public string anim;
    public string sound;

    public Attack(string name, int damage, float ejection, Vector2 direction, GameObject hitbox, float time, string anim, string sound)
    {
        this.name = name;
        this.damage = damage;
        this.ejection = ejection;
        this.direction = direction;
        this.hitbox = hitbox;
        this.time = time;
        this.anim = anim;
        this.sound = sound;
    }
}
