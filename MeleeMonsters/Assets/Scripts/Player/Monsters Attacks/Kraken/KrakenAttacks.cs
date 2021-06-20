using System.Collections;
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
    public GameObject geyserPrefab;


    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();
        attacks.Add(new Attack("SideGround", 4, 350, new Vector2(1, 0.1f), 0.25f,"Sg", "wet slap", 1f));
        attacks.Add(new Attack("DownGround", 4, 400,  new Vector2(0, 1), 0.30f, "Dg", "", 1f));
        attacks.Add(new Attack("NeutralGround", 5, 300, new Vector2(0, 1), 0.32f, "Ng", "", 1f));
        
        attacks.Add(new Attack("SideAir", 4, 350, new Vector2(0, 1), 0.25f, "Sa", "wet slap", 1f));
        attacks.Add(new Attack("DownAir", 7, 500, new Vector2(0, -1), 0.37f, "Da", "", 1f));
        attacks.Add(new Attack("NeutralAir", 6, 400, new Vector2(0, 1), 0.35f, "Na", "", 1f));
        
        attacks.Add(new Attack("SideSpecial", 10, 600, new Vector2(1, 0), 1f, "Ss", "", 1f));
        attacks.Add(new Attack("DownSpecial", 8, 400,  new Vector2(0, 1), 0.1f,"", "", 1f));
        attacks.Add(new Attack("NeutralSpecial", 3, 100, new Vector2(0, 1), 0.1f,"Ns", "", 1f));
        
        attacks.Add(new Attack("SideWrath", 12, 850, new Vector2(1, 0), 0.1f,"Sw", "", 1f));
        attacks.Add(new Attack("DownWrath", 14, 1000,  new Vector2(0, 1), 1f, "Dw", "", 1f));
        attacks.Add(new Attack("NeutralWrath", 5, 200, new Vector2(0, 1), 0.1f, "Nw", "", 1f));
        
    }


    //Basic attacks
    public override void SideGround(){}
    public override void DownGround(){}
    public override void NeutralGround(){}
    public override void SideAir(){}

    public override void DownAir()
    {
        GameObject bubble = PhotonNetwork.Instantiate(bubblePrefab.name, hitboxesPoints[4].position, new Quaternion());
        bubble.GetComponent<KrakenBubble>().Throw(0, gameObject, 0.5f);
    }


    public override void NeutralAir(){}



    public override void SideSpecial(){}

    public override void DownSpecial()
    {
        pM.SetFastFallAttack();
    }

    public override void NeutralSpecial()
    {
        if(gameObject.tag == "IA")
        {
            GameObject bubble = PhotonNetwork.Instantiate(bubblePrefab.name, hitboxesPoints[1].position, new Quaternion());
            bubble.GetComponent<KrakenBubble>().Throw(pM.direction, gameObject, 0);
        }
        else
        {
            if (receiveTime)
            {
                float deltaTime = pA.specialTimeFinished - pA.specialTimeStarted;
                GameObject bubble = PhotonNetwork.Instantiate(bubblePrefab.name, hitboxesPoints[1].position, new Quaternion());
                bubble.GetComponent<KrakenBubble>().Throw(pM.direction, gameObject, deltaTime);
                receiveTime = false;
                neutralSpecial = false;
            }
            else
                neutralSpecial = true;
        }
        
    }

   


    public override void SideWrath()
    {
        GameObject wave = PhotonNetwork.Instantiate(wavePrefab.name, hitboxesPoints[0].position, new Quaternion());
        wave.GetComponent<KrakenWave>().Throw(pM.direction, gameObject);
    }

    public override void DownWrath(){}

    public override void NeutralWrath()
    {
        if(gameObject.tag == "IA")
        {
            GameObject bubbleWrath = PhotonNetwork.Instantiate(bubbleWrathPrefab.name, hitboxesPoints[1].position, new Quaternion());
            bubbleWrath.GetComponent<KrakenWrathBubble>().Throw(pM.direction, gameObject, 0);
        }
        else
        {
            if (receiveTime)
            {
                float deltaTime = pA.specialTimeFinished - pA.specialTimeStarted;
                GameObject bubbleWrath = PhotonNetwork.Instantiate(bubbleWrathPrefab.name, hitboxesPoints[1].position, new Quaternion());
                bubbleWrath.GetComponent<KrakenWrathBubble>().Throw(pM.direction, gameObject, deltaTime);
                receiveTime = false;
                neutralSpecial = false;
            }
            else
            {
                neutralSpecial = true;
            }
        }
    }


    public override void FastFallAttackCallback() 
    {
        print("callback pour l'" + gameObject.name);
        pA.PlayAttackAnimation("Ds");
        GameObject hiddenGeyserLeft = PhotonNetwork.Instantiate(geyserPrefab.name, hitboxesPoints[2].position, new Quaternion());
        GameObject hiddenGeyserRight = PhotonNetwork.Instantiate(geyserPrefab.name, hitboxesPoints[3].position, new Quaternion());
        hiddenGeyserLeft.GetComponent<KrakenHiddenGeyser>().Throw(-1, gameObject);
        hiddenGeyserRight.GetComponent<KrakenHiddenGeyser>().Throw(1, gameObject);
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}


