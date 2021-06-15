﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostAttacks : MonstersAttacks, IPunObservable
{
    public GameObject batPrefab;
    public GameObject wrathBatPrefab;

    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();

        attacks.Add(new Attack("SideGround", 3, 300, new Vector2(1, 0.1f), 0.1f,"Sg",""));
        attacks.Add(new Attack("DownGround", 5, 400, new Vector2(0, 1), 0.1f,"Dg", ""));
        attacks.Add(new Attack("NeutralGround", 4, 150,  new Vector2(0, 1), 0.1f,"Ng", ""));

        attacks.Add(new Attack("SideAir", 5, 400,  new Vector2(1, 0), 0.1f, "Sa", ""));
        attacks.Add(new Attack("DownAir", 6, 200,  new Vector2(0, 1), 0.1f,"Da", ""));
        attacks.Add(new Attack("NeutralAir", 2, 100,  new Vector2(0, 1), 0.1f,"Na", ""));

        attacks.Add(new Attack("SideSpecial", 5, 100, new Vector2(1, 0), 0.1f, "Ss", ""));
        attacks.Add(new Attack("DownSpecial", 6, 100,  new Vector2(0, 1), 0.6f, "Ds", ""));
        attacks.Add(new Attack("NeutralSpecial", 2, 500, new Vector2(1, 0.1f), 0.6f, "Ns", ""));

        attacks.Add(new Attack("SideWrath", 8, 250,  new Vector2(1, 0), 0.1f,"Sw", ""));
        attacks.Add(new Attack("DownWrath", 10, 200,  new Vector2(0, 1),  1f, "Dw", ""));
        attacks.Add(new Attack("NeutralWrath", 3, 700, new Vector2(1, 0.1f), 0.8f, "Nw", ""));
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
        print("je fais une sidespecial de fantome ohlalah");
    }

    public override void DownSpecial()
    {
        print("je fais une downSpecial de fantome wouaaaa");
        GameObject bat = PhotonNetwork.Instantiate(batPrefab.name, hitboxesPoints[0].position, new Quaternion());
        bat.GetComponent<GhostBat>().Throw(pM.direction, gameObject);
    }

    public override void NeutralSpecial(){}



    public override void SideWrath()
    {

    }

    public override void DownWrath()
    {
        GameObject wrathBat = PhotonNetwork.Instantiate(wrathBatPrefab.name, hitboxesPoints[0].position, new Quaternion());
        wrathBat.GetComponent<GhostWrathBat>().Throw(pM.direction, gameObject);
    }

    public override void NeutralWrath()
    {

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
