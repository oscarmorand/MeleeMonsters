using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostAttacks : MonstersAttacks, IPunObservable
{
    public GameObject batPrefab;

    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();
        attacks.Add(new Attack("SideGround", 3, 300, 0.5f, new Vector2(1, 0.1f), hitboxesPoints[0], new Vector2(1, 1.3f)));
        attacks.Add(new Attack("DownGround", 5, 400, 0.7f, new Vector2(0, 1), hitboxesPoints[1], new Vector2(3.5f, 0.5f)));
        attacks.Add(new Attack("NeutralGround", 4, 150, 0.3f, new Vector2(0, 1), hitboxesPoints[2], new Vector2(0.8f, 2.2f)));
        
        attacks.Add(new Attack("SideAir", 5, 400, 0.4f, new Vector2(1, 0), hitboxesPoints[3], new Vector2(1.3f, 1.3f)));
        attacks.Add(new Attack("DownAir", 6, 200, 0.8f, new Vector2(0, 1), hitboxesPoints[4], new Vector2(0.7f, 1.45f)));
        attacks.Add(new Attack("NeutralAir", 2, 100, 0.2f, new Vector2(0, 1), hitboxesPoints[5], new Vector2(2.5f, 2.5f)));
       
        attacks.Add(new Attack("SideSpecial", 5, 500, 1f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownSpecial", 6, 100, 1.2f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralSpecial", 3, 800, 0.7f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        
        attacks.Add(new Attack("SideWrath", 8, 650, 0.8f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownWrath", 10, 200, 1.5f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralWrath", 5, 1000, 1f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
    }



    public override void SideGround()
    {
        pA.BasicAttack(attacks[0]);
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
        print("je fais une sidespecial de fantome ohlalah");
    }

    public override void DownSpecial()
    {
        print("je fais une downSpecial de fantome wouaaaa");
        GameObject bat = PhotonNetwork.Instantiate(batPrefab.name, hitboxesPoints[0].position, new Quaternion());
        bat.GetComponent<GhostBat>().Throw(pM.direction, gameObject);
    }

    public override void NeutralSpecial()
    {
        print("je fais une neutralspecial de fantom hannnn");
    }



    public override void SideWrath()
    {

    }

    public override void DownWrath()
    {

    }

    public override void NeutralWrath()
    {

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
