using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string name;
    public int damage;
    public float ejection;
    public float time;
    public Vector2 direction;
    public Transform hitBox;
    public Vector2 size;

    public Attack(string name, int damage,float ejection, float time, Vector2 direction, Transform hitBox, Vector2 size)
    {
        this.name = name;
        this.damage = damage;
        this.ejection = ejection;
        this.time = time;
        this.direction = direction;
        this.hitBox = hitBox;
        this.size = size;
    }
}
