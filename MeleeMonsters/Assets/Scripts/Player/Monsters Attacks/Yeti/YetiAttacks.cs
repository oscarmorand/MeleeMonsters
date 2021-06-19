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

        attacks.Add(new Attack("SideGround", 6, 400, new Vector2(1, 0.1f), 0.3f,"Sg", ""));
        attacks.Add(new Attack("DownGround", 9, 500,  new Vector2(0, 1), 0.35f,"Dg", ""));
        attacks.Add(new Attack("NeutralGround", 4, 200, new Vector2(0, 1), 0.3f,"Ng", ""));


        attacks.Add(new Attack("SideAir", 10, 600,  new Vector2(0.5f, -1), 0.4f,"Sa", ""));
        attacks.Add(new Attack("DownAir", 11, 700, new Vector2(0, -1), 0.42f, "Da", ""));
        attacks.Add(new Attack("NeutralAir", 6, 350,  new Vector2(0, 1), 0.15f,"Na", ""));


        attacks.Add(new Attack("SideSpecial", 4, 150, new Vector2(1, 0), 0.6f,"Ss", ""));
        attacks.Add(new Attack("DownSpecial", 12, 60,  new Vector2(1, 1), 0.7f,"Ds", ""));
        attacks.Add(new Attack("NeutralSpecial", 7, 500, new Vector2(0, 1), 1f,"Ns", ""));


        attacks.Add(new Attack("SideWrath", 10, 400,new Vector2(1, 0), 0.8f,"Sw", ""));
        attacks.Add(new Attack("DownWrath", 15, 100, new Vector2(0, 1), 1f,"Dw", "")); // fait mal mais n'éjecte pas trop haut
        attacks.Add(new Attack("NeutralWrath", 10, 600, new Vector2(0, 1), 2f,"Nw", "")); //éjecte assez haut

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
        pM.SetDashAttackState(0.2f, 0.03f, 18f, new Vector2(0.5f, 1));
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
        pM.SetDashAttackState(0.2f, 0.03f, 18f, new Vector2(0f, 1));
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
