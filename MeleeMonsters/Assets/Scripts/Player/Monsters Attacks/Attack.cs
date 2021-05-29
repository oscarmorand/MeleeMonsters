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
    public float activationTime;
    public float durationTime;
    public float disabledTime;
    public float hitStunTime;
    public string anim;

    public Attack(string name, int damage, float ejection, Vector2 direction, GameObject hitbox, float activationTime,float durationTime, float disabledTime,float hitStunTime,string anim)
    {
        this.name = name;
        this.damage = damage;
        this.ejection = ejection;
        this.direction = direction;
        this.hitbox = hitbox;
        this.activationTime = activationTime;
        this.durationTime = durationTime;
        this.disabledTime = disabledTime;
        this.hitStunTime = hitStunTime;
        this.anim = anim;
    }
}
