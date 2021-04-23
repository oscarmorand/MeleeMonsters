using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingButton : MonoBehaviour
{

    private PlayerAttacks pAt;
    private KrakenAttacks kA;

    // Start is called before the first frame update
    void Start()
    {
        pAt = GetComponent<PlayerAttacks>();
        kA = GetComponent<KrakenAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pAt.stoppedPressing && kA.neutralSpecial)
        {
            kA.receiveTime = true;
            kA.NeutralSpecial();
            pAt.stoppedPressing = false;
        }
    }
}
