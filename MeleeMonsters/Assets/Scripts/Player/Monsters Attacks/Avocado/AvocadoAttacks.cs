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

        attacks.Add(new Attack("SideGround", 4, 150, new Vector2(1, 0.1f), 0.3f,"Sg", "slash sabre"));
        attacks.Add(new Attack("DownGround", 5, 150, new Vector2(0, 1), 0.3f,"Dg", "slash sabre"));
        attacks.Add(new Attack("NeutralGround", 3, 100, new Vector2(0, 1), 0.3f,"Ng","slash sabre"));


        attacks.Add(new Attack("SideAir", 4, 350, new Vector2(1, 0),  0.1f,"Sa","slash sabre"));
        attacks.Add(new Attack("DownAir", 6, 250, new Vector2(0, -1), 0.1f,"Da","slash sabre"));
        attacks.Add(new Attack("NeutralAir", 3, 300, new Vector2(0, 1), 0.1f,"Na","slash sabre"));


        attacks.Add(new Attack("SideSpecial", 7, 500, new Vector2(1, 0.2f), 0.1f, "",""));
        attacks.Add(new Attack("DownSpecial", 6, 400, new Vector2(0, 1), 0.1f, "",""));
        attacks.Add(new Attack("NeutralSpecial", 2, 50, new Vector2(0, 1), 0.4f, "",""));


        attacks.Add(new Attack("SideWrath", 10, 750, new Vector2(1, 0), 0.1f, "",""));
        attacks.Add(new Attack("DownWrath", 9, 500, new Vector2(0, 1), 0.1f, "",""));
        attacks.Add(new Attack("NeutralWrath", 4, 75, new Vector2(0, 1), 0.1f,"",""));

    }

    //Basic attacks
    public override void SideGround() { }
    public override void DownGround() { }
    public override void NeutralGround() { }
    public override void SideAir() { }
    public override void DownAir() { }
    public override void NeutralAir() { }



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
        aM.Play("bouchon champagne");
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
        aM.Play("bouchon champagne");
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

    /*
    /// <summary>
    /// Joue un son de couteau / sabre au hasard
    /// </summary>
    private void PlayRandomKnifeSound()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            aM.Play("slash sabre");
        if (rand == 1)
            aM.Play("slash couteau 1");
        if (rand == 2)
            aM.Play("slash couteau 2");
    }
    */


    //attacks.Add(new Attack("SideGround", 4, 350, new Vector2(1, 0.1f), hitboxesPoints[0], new Vector2(1.3f, 0.75f), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("DownGround", 5, 150,  new Vector2(0, 1), hitboxesPoints[1], new Vector2(1, 1), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("NeutralGround", 3, 100, new Vector2(0, 1), hitboxesPoints[2], new Vector2(1.9f, 0.55f), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));


    //attacks.Add(new Attack("SideAir", 4, 350, new Vector2(1, 0), hitboxesPoints[3], new Vector2(1, 2), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("DownAir", 6, 250, new Vector2(0, -1), hitboxesPoints[4], new Vector2(0.65f, 1.1f), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("NeutralAir", 3, 300, new Vector2(0, 1), hitboxesPoints[5], new Vector2(1.9f, 0.7f), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));


    //attacks.Add(new Attack("SideSpecial", 7, 500, new Vector2(1, 0.2f), hitboxesPoints[0], new Vector2(1, 1), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("DownSpecial", 6, 400, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("NeutralSpecial", 2, 50, new Vector2(0, 1), hitboxesPoints[5], new Vector2(1, 1), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));


    //attacks.Add(new Attack("SideWrath", 10, 750, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("DownWrath", 9, 500, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));
    //attacks.Add(new Attack("NeutralWrath", 4, 75, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1), hitboxes[0], 0.1f, 0.2f, 0.1f, 0.2f));

   
}
