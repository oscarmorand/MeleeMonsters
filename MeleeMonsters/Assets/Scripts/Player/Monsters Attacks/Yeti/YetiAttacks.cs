using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YetiAttacks : MonstersAttacks, IPunObservable
{

    public GameObject snowballPrefab;
    public GameObject iceCubePrefab;
    public GameObject hiddenStalactitePrefab;

    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();

        attacks.Add(new Attack("SideGround", 9, 550, new Vector2(2, 1f), 0.6f,"Sg", "wooshe4",1f));
        attacks.Add(new Attack("DownGround", 12, 610,  new Vector2(0, 1), 0.7f,"Dg", "wooshe7", 1f));
        attacks.Add(new Attack("NeutralGround", 14, 400, new Vector2(0, 2), 0.9f,"Ng", "wooshe15", 1f));


        attacks.Add(new Attack("SideAir", 10, 550,  new Vector2(1, -1), 0.6f,"Sa", "wooshe4", 1f));
        attacks.Add(new Attack("DownAir", 11, 550, new Vector2(0, -2), 0.6f, "Da", "wooshe15", 1f));
        attacks.Add(new Attack("NeutralAir", 14, 400,  new Vector2(2, 0), 0.9f,"Na", "wooshe15", 1f));


        attacks.Add(new Attack("SideSpecial", 4, 100, new Vector2(1, 0), 0.6f,"Ss", "", 0.3f));
        attacks.Add(new Attack("DownSpecial", 12, 500,  new Vector2(0, 2), 1.3f,"Ds", "icebreak", 0.7f));
        attacks.Add(new Attack("NeutralSpecial", 10, 550, new Vector2(0, 2), 1.4f,"Ns", "wooshb4", 0.7f));


        attacks.Add(new Attack("SideWrath", 10, 100,new Vector2(1, 1), 0.8f,"Sw", "", 0.3f));
        attacks.Add(new Attack("DownWrath", 22, 580, new Vector2(0, 1), 1f,"Dw", "wooshb4", 1f)); // fait mal mais n'éjecte pas trop haut
        attacks.Add(new Attack("NeutralWrath", 18, 650, new Vector2(1, 2), 2f,"Nw", "wooshb4", 1f)); //éjecte assez haut

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
        GameObject snowball = PhotonNetwork.Instantiate(snowballPrefab.name, hitboxesPoints[0].position, new Quaternion());
        snowball.GetComponent<YetiSnowball>().Throw(pM.direction, gameObject);
    }

    public override void DownSpecial(){}

    public override void NeutralSpecial()
    {
        pM.SetDashAttackState(0.2f, 0.02f, 12f, new Vector2(1, 1));
    }

    

    public override void SideWrath()
    {
        GameObject iceCube = PhotonNetwork.Instantiate(iceCubePrefab.name, hitboxesPoints[0].position, new Quaternion());
        iceCube.GetComponent<YetiIcecube>().Throw(pM.direction, gameObject);
    }

    public override void DownWrath()
    {
        pM.SetFastFallAttack();
    }

    public override void NeutralWrath()
    {
        pM.SetDashAttackState(0.2f, 0.02f, 12f, new Vector2(0f, 1));
    }


    public override void FastFallAttackCallback()
    {
        GameObject hiddenStalactiteLeft = PhotonNetwork.Instantiate(hiddenStalactitePrefab.name, hitboxesPoints[1].position, new Quaternion());
        GameObject hiddenStalactiteRight = PhotonNetwork.Instantiate(hiddenStalactitePrefab.name, hitboxesPoints[2].position, new Quaternion());
        hiddenStalactiteLeft.GetComponent<YetiHiddenStalactite>().Throw(-1, gameObject);
        hiddenStalactiteRight.GetComponent<YetiHiddenStalactite>().Throw(1, gameObject);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
