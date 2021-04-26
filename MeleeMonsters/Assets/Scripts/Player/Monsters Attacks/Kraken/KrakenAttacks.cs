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


    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();
        attacks.Add(new Attack("SideGround", 4, 350, 0.5f, new Vector2(1, 0.1f), hitboxesPoints[0], new Vector2(1.5f, 0.75f)));
        attacks.Add(new Attack("DownGround", 4, 400, 0.6f, new Vector2(0, 1), hitboxesPoints[1], new Vector2(3.85f, 0.7f)));
        attacks.Add(new Attack("NeutralGround", 5, 300, 0.3f, new Vector2(0, 1), hitboxesPoints[2], new Vector2(1.3f, 0.7f)));
        
        attacks.Add(new Attack("SideAir", 4, 350, 0.4f, new Vector2(0, 1), hitboxesPoints[3], new Vector2(1, 1.85f)));
        attacks.Add(new Attack("DownAir", 7, 500, 0.7f, new Vector2(0, -1), hitboxesPoints[4], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralAir", 6, 400, 0.6f, new Vector2(0, 1), hitboxesPoints[5], new Vector2(2.5f, 3.1f)));
        
        attacks.Add(new Attack("SideSpecial", 10, 600, 1f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownSpecial", 8, 400, 0.7f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralSpecial", 3, 100, 0.3f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        
        attacks.Add(new Attack("SideWrath", 12, 850, 1.3f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownWrath", 14, 1000, 1.5f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralWrath", 5, 200, 0.6f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        
    }

    public override void SideGround()
    {
        pA.BasicAttack(attacks[0]);
        aM.Play("wet slap");
    }

    public override void DownGround()
    {
        pA.BasicAttack(attacks[1]);
    }


    public override void NeutralGround()
    {
        pA.BasicAttack(attacks[2]);
    }

    


    public override void SideAir()
    {
        pA.BasicAttack(attacks[3]);
        aM.Play("wet slap");
    }

    public override void DownAir()
    {
        pA.BasicAttack(attacks[4]);
    }

    public override void NeutralAir()
    {
        pA.BasicAttack(attacks[5]);
    }

    

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


