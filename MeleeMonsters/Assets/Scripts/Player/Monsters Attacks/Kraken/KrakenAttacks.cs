﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KrakenAttacks : MonstersAttacks, IPunObservable
{

    internal bool receiveTime = false;
    internal bool neutralSpecial = false;

    public GameObject bubblePrefab;
    public GameObject bubbleWrathPrefab;
    public GameObject wavePrefab;


    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();
        attacks.Add(new Attack("SideGround", 4, 350, new Vector2(1, 0.1f), 0.1f,"Sg", "wet slap"));
        attacks.Add(new Attack("DownGround", 4, 400,  new Vector2(0, 1), 0.1f, "Dg", ""));
        attacks.Add(new Attack("NeutralGround", 5, 300, new Vector2(0, 1), 0.1f, "Ng", ""));
        
        attacks.Add(new Attack("SideAir", 4, 350, new Vector2(0, 1), 0.1f, "Sa", "wet slap"));
        attacks.Add(new Attack("DownAir", 7, 500, new Vector2(0, -1), 0.1f, "Da", ""));
        attacks.Add(new Attack("NeutralAir", 6, 400, new Vector2(0, 1), 0.1f, "Na", ""));
        
        attacks.Add(new Attack("SideSpecial", 10, 600, new Vector2(1, 0), 0.1f, "Ss", ""));
        attacks.Add(new Attack("DownSpecial", 8, 400,  new Vector2(0, 1), 0.1f,"Ds", ""));
        attacks.Add(new Attack("NeutralSpecial", 3, 100, new Vector2(0, 1), 0.1f,"Ns", ""));
        
        attacks.Add(new Attack("SideWrath", 12, 850, new Vector2(1, 0), 0.1f,"Sw", ""));
        attacks.Add(new Attack("DownWrath", 14, 1000,  new Vector2(0, 1), 0.1f, "Dw", ""));
        attacks.Add(new Attack("NeutralWrath", 5, 200, new Vector2(0, 1), 0.1f, "Nw", ""));
        
    }


    //Basic attacks
    public override void SideGround(){}
    public override void DownGround(){}
    public override void NeutralGround(){}
    public override void SideAir(){}
    public override void DownAir(){}
    public override void NeutralAir(){}



    public override void SideSpecial()
    {
        print("je fais une sidespecial de kraken ohlalah");
    }

    public override void DownSpecial()
    {
        print("je fais une downspecial de kraken hannnn");
    }

    public override void NeutralSpecial()
    {
        print("je fais une neutralspecial de kraken hannnn");
        if (receiveTime)
        {
            float deltaTime = pA.specialTimeFinished - pA.specialTimeStarted;
            GameObject bubble = PhotonNetwork.Instantiate(bubblePrefab.name, hitboxesPoints[3].position, new Quaternion());
            bubble.GetComponent<KrakenBubble>().Throw(pM.direction, gameObject, deltaTime);
            receiveTime = false;
            neutralSpecial = false;
        }
        else
        {
            neutralSpecial = true;
        }
    }

   


    public override void SideWrath()
    {

    }

    public override void DownWrath()
    {
        GameObject wave = PhotonNetwork.Instantiate(wavePrefab.name, hitboxesPoints[0].position, new Quaternion());
        wave.GetComponent<KrakenWave>().Throw(pM.direction, gameObject);
    }

    public override void NeutralWrath()
    {
        if (receiveTime)
        {
            float deltaTime = pA.specialTimeFinished - pA.specialTimeStarted;
            GameObject bubbleWrath = PhotonNetwork.Instantiate(bubbleWrathPrefab.name, hitboxesPoints[3].position, new Quaternion());
            bubbleWrath.GetComponent<KrakenWrathBubble>().Throw(pM.direction, gameObject, deltaTime);
            receiveTime = false;
            neutralSpecial = false;
        }
        else
        {
            neutralSpecial = true;
        }
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}


