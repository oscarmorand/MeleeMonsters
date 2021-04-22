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
        attacks.Add(new Attack("SideWeak", 4, 400, 0.4f, new Vector2(1, 0.5f), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralWeak", 3, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownWeak", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));

        attacks.Add(new Attack("SideSpecial", 10, 100, 0.8f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralSpecial", 2, 400, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownSpecial", 5, 250, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));

        attacks.Add(new Attack("SideAir", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralAir", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[2], new Vector2(1, 1)));
        attacks.Add(new Attack("DownAir", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[1], new Vector2(1, 1)));

        attacks.Add(new Attack("SideWrath", 10, 100, 0.8f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralWrath", 2, 400, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownWrath", 5, 250, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
    }

    public override void SideGround()
    {
        pA.BasicAttack(attacks[0]);
    }

    public override void NeutralGround()
    {
        pA.BasicAttack(attacks[1]);
    }

    public override void DownGround()
    {
        pA.BasicAttack(attacks[2]);
    }

    public override void SideSpecial()
    {
        GameObject snowball = PhotonNetwork.Instantiate(snowballPrefab.name, hitboxesPoints[0].position, new Quaternion());
        snowball.GetComponent<YetiSnowball>().Throw(pM.direction, gameObject);
    }

    public override void NeutralSpecial()
    {
        print("je fais une neutralspecial de yeti hannnn");
    }

    public override void DownSpecial()
    {
        print("je fais une downSpecial de yeti wouaaaa");
    }

    public override void SideAir()
    {
        pA.BasicAttack(attacks[6]);
    }

    public override void NeutralAir()
    {
        pA.BasicAttack(attacks[7]);
    }

    public override void DownAir()
    {
        pA.BasicAttack(attacks[8]);
    }

    public override void SideWrath()
    {
        GameObject iceCube = PhotonNetwork.Instantiate(iceCubePrefab.name, hitboxesPoints[0].position, new Quaternion());
        iceCube.GetComponent<YetiIcecube>().Throw(pM.direction, gameObject);
    }

    public override void NeutralWrath()
    {

    }

    public override void DownWrath()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
