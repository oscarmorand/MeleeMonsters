using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string name;
    public int damage;
    public float ejection;
    public Vector2 direction;
    public float time;
    public string anim;
    public string sound;

    public Attack(string name, int damage, float ejection, Vector2 direction, float time, string anim, string sound)
    {
        this.name = name;
        this.damage = damage;
        this.ejection = ejection;
        this.direction = direction;
        this.time = time;
        this.anim = anim;
        this.sound = sound;
    }

    public static Attack Deserialize(float[] data)
    {
        Attack result = new Attack("", (int)data[0], data[1], new Vector2(data[2], data[3]), 0, "","") ;
        return result;
    }

    public static float[] Serialize(Attack attack)
    {
        float[] result = new float[4];
        result[0] = (float)attack.damage;
        result[1] = attack.ejection;
        result[2] = attack.direction.x;
        result[3] = attack.direction.y;
        return result;
    }
}
