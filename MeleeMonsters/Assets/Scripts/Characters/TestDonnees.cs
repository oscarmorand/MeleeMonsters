using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestDonnees
{
    private static int monsterType;

    public static int MonsterType
    {
        get
        {
            return monsterType;
        }
        set
        {
            monsterType = value;
        }
    }
}
