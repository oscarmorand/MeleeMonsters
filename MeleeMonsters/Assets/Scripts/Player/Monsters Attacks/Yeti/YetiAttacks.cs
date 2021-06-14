using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YetiAttacks : MonstersAttacks, IPunObservable
{

    public GameObject snowballPrefab;
    public GameObject iceCubePrefab;

    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();
        //attacks.Add(new Attack("SideGround", 6, 400, 0.8f, new Vector2(1, 0.1f), hitboxesPoints[0], new Vector2(1.15f, 0.8f)));
        //attacks.Add(new Attack("DownGround", 9, 500, 1f, new Vector2(0, 1), hitboxesPoints[1], new Vector2(2.5f, 1)));
        //attacks.Add(new Attack("NeutralGround", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[2], new Vector2(1, 0.5f)));


        //attacks.Add(new Attack("SideAir", 10, 600, 1f, new Vector2(0.5f, -1), hitboxesPoints[3], new Vector2(1.5f, 1)));
        //attacks.Add(new Attack("DownAir", 11, 700, 1.2f, new Vector2(0, -1), hitboxesPoints[4], new Vector2(1.39f, 1)));
        //attacks.Add(new Attack("NeutralAir", 6, 350, 0.5f, new Vector2(0, 1), hitboxesPoints[5], new Vector2(1, 0.85f)));


        //attacks.Add(new Attack("SideSpecial", 4, 150, 0.6f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        //attacks.Add(new Attack("DownSpecial", 12, 900, 1.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        //attacks.Add(new Attack("NeutralSpecial", 15, 1200, 1.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));


        //attacks.Add(new Attack("SideWrath", 10, 400, 1f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        //attacks.Add(new Attack("DownWrath", 15, 1000, 1.9f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        //attacks.Add(new Attack("NeutralWrath", 20, 1500, 2f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));

        attacks.Add(new Attack("SideGround", 6, 400, new Vector2(1, 0.1f), hitboxes[0], 0.1f,"Sg", ""));
        attacks.Add(new Attack("DownGround", 9, 500,  new Vector2(0, 1), hitboxes[0], 0.1f,"Dg", ""));
        attacks.Add(new Attack("NeutralGround", 4, 200, new Vector2(0, 1), hitboxes[0], 0.1f,"Ng", ""));


        attacks.Add(new Attack("SideAir", 10, 600,  new Vector2(0.5f, -1), hitboxes[0], 0.1f,"Sa", ""));
        attacks.Add(new Attack("DownAir", 11, 700, new Vector2(0, -1), hitboxes[0], 0.1f, "Da", ""));
        attacks.Add(new Attack("NeutralAir", 6, 350,  new Vector2(0, 1), hitboxes[0], 0.1f,"Na", ""));


        attacks.Add(new Attack("SideSpecial", 4, 150, new Vector2(1, 0), null, 0.1f,"", ""));
        attacks.Add(new Attack("DownSpecial", 12, 900,  new Vector2(0, 1), null, 0.1f,"", ""));
        attacks.Add(new Attack("NeutralSpecial", 15, 1200, new Vector2(0, 1), null, 0.1f,"", ""));


        attacks.Add(new Attack("SideWrath", 10, 400,new Vector2(1, 0), null, 0.1f,"", ""));
        attacks.Add(new Attack("DownWrath", 15, 1000, new Vector2(0, 1), null, 0.1f,"", ""));
        attacks.Add(new Attack("NeutralWrath", 20, 1500, new Vector2(0, 1), null, 0.1f,"", ""));

    }

    //public override void SideGround()
    //{
    //    pA.BasicAttack(attacks[0]);
    //}

    //public override void DownGround()
    //{
    //    pA.BasicAttack(attacks[1]);
    //}

    //public override void NeutralGround()
    //{
    //    pA.BasicAttack(attacks[2]);
    //}
   

    //public override void SideAir()
    //{
    //    pA.BasicAttack(attacks[3]);
    //}

    //public override void DownAir()
    //{
    //    pA.BasicAttack(attacks[4]);
    //}

    //public override void NeutralAir()
    //{
    //    pA.BasicAttack(attacks[5]);
    //}
   

  

    public override void SideSpecial()
    {
        GameObject snowball = PhotonNetwork.Instantiate(snowballPrefab.name, hitboxesPoints[0].position, new Quaternion());
        snowball.GetComponent<YetiSnowball>().Throw(pM.direction, gameObject);
    }

    public override void DownSpecial()
    {
        print("je fais une downSpecial de yeti wouaaaa");
    }

    public override void NeutralSpecial()
    {
        print("je fais une neutralspecial de yeti hannnn");
    }

    

    public override void SideWrath()
    {
        GameObject iceCube = PhotonNetwork.Instantiate(iceCubePrefab.name, hitboxesPoints[0].position, new Quaternion());
        iceCube.GetComponent<YetiIcecube>().Throw(pM.direction, gameObject);
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
