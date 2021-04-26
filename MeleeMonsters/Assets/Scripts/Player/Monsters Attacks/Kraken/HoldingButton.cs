using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingButton : MonoBehaviour
{

    private PlayerAttacks pAt;
    private KrakenAttacks kA;
    private PlayerScript pS;

    // Start is called before the first frame update
    void Start()
    {
        pAt = GetComponent<PlayerAttacks>();
        pS = GetComponent<PlayerScript>();
        kA = GetComponent<KrakenAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pAt.stoppedPressing && kA.neutralSpecial)
        {
            kA.receiveTime = true;
            if (!pS.isWrath)
                kA.NeutralSpecial();
            else
                kA.NeutralWrath();
            pAt.stoppedPressing = false;
        }
    }
}
