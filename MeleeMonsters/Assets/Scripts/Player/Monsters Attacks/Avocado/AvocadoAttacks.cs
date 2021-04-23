using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoAttacks : MonstersAttacks, IPunObservable
{

    public GameObject bulletPrefab;
    public GameObject bulletWrathPrefab;

    public SpriteRenderer noyau;
    


    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();

        attacks.Add(new Attack("SideGround", 4, 400, 0.4f, new Vector2(1, 0.5f), hitboxesPoints[0], new Vector2(1.3f, 0.75f)));
        attacks.Add(new Attack("DownGround", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[1], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralGround", 3, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[2], new Vector2(1.9f, 0.55f)));
       
        
        attacks.Add(new Attack("SideAir", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[3], new Vector2(1, 2)));
        attacks.Add(new Attack("DownAir", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[4], new Vector2(0.65f, 1.1f)));
        attacks.Add(new Attack("NeutralAir", 4, 200, 0.4f, new Vector2(0, 1), hitboxesPoints[5], new Vector2(1.9f, 0.55f)));
        
        
        attacks.Add(new Attack("SideSpecial", 10, 100, 0.8f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownSpecial", 5, 250, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralSpecial", 2, 400, 0.8f, new Vector2(0, 1), hitboxesPoints[5], new Vector2(1, 1)));
       

        attacks.Add(new Attack("SideWrath", 10, 100, 0.8f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("DownWrath", 5, 250, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("NeutralWrath", 2, 400, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        
    }

    public override void SideGround()
    {
        pA.BasicAttack(attacks[0]);
        pAn.Attack("Sg");
    }

    public override void DownGround()
    {
        pA.BasicAttack(attacks[1]);
    }

    public override void NeutralGround()
    {
        pA.BasicAttack(attacks[2]);
        pAn.Attack("Ng");
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
        print("je fais une sidespecial d'avocat ohlalah");
    }


    public override void DownSpecial()
    {
        print("je fais une downSpecial d'avocat wouaaaa");
    }

    public override void NeutralSpecial()
    {
        print("je fais une neutralspecial d'avocat hannnn");
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, hitboxesPoints[6].position, new Quaternion());
        bullet.GetComponent<AvocadoBullet>().Throw(pM.direction, gameObject);
        NoyauDisable();
    }

  



    public override void SideWrath()
    {

    }

    public override void DownWrath()
    {

    }

    public override void NeutralWrath()
    {
        print("je fais une neutralspecial d'avocat hannnn");
        GameObject bullet = PhotonNetwork.Instantiate(bulletWrathPrefab.name, hitboxesPoints[6].position, new Quaternion());
        bullet.GetComponent<AvocadoWrathBullet>().Throw(pM.direction, gameObject);
        NoyauDisable();
    }

    



    public void NoyauDisable()
    {
        noyau.enabled = false;
        Invoke("NoyauEnable", 0.8f);
    }

    public void NoyauEnable()
    {
        noyau.enabled = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(noyau.enabled);
        }
        else
        {
            noyau.enabled = (bool)stream.ReceiveNext();
        }
    }
}
