using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSoundManager : MonoBehaviour
{
    public List<PlayerScript> players = new List<PlayerScript>();
    //public List<PlayerScript> truePlayers = new List<PlayerScript>();
    private AudioManager aM;

    private int lastHowMany = 0;
    public int howManyInWrathMode = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayers();
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    //
    void LateUpdate()
    {
        if (players.Count < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            ActualizePlayers();
        }

        int localInt = 0;
        foreach (PlayerScript playerScript in players)
        {
            if (playerScript.isWrath)
                localInt += 1;
        }
        howManyInWrathMode = localInt;

        if(howManyInWrathMode == 0)
        {
            if(lastHowMany > 0)
            {
                aM.Stop("wrath theme");
                aM.UnPause("theme");
            }
        }
        else if(howManyInWrathMode > 0)
        {
            if(lastHowMany == 0)
            {
                aM.Pause("theme");
                aM.Play("wrath theme");
            }
        }

        lastHowMany = howManyInWrathMode;
    }


    void ActualizePlayers()
    {
        players = new List<PlayerScript>();
        InitializePlayers();
    }

    void InitializePlayers()
    {
        GameObject[] playersGameObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] IAGameObjects = GameObject.FindGameObjectsWithTag("IA");

        foreach (GameObject playerGameObject in playersGameObjects)
        {
            players.Add(playerGameObject.GetComponent<PlayerScript>());
            //truePlayers.Add(playerGameObject.GetComponent<PlayerScript>());
        }

        foreach (GameObject IAGameObject in IAGameObjects)
        {
            players.Add(IAGameObject.GetComponent<PlayerScript>());
        }
    }
}
