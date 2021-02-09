using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocat : PlayerClass
{
    // Start is called before the first frame update
    void Start()
    {
        FaitesDuBruitCeSoiiir();
        this.speed = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FaitesDuBruitCeSoiiir()
    {
        print("ouai l'avocat là");
    }
}
