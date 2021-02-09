﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int monsterType;

    public int MonsterType { get { return monsterType; } }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        monsterType = Random.Range(0, 2);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
