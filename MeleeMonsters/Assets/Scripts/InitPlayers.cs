using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InitPlayers : MonoBehaviourPunCallbacks
{
    public GameObject avocatPrefab;
    public GameObject fantomePrefab;

    private GameObject playerPrefab;

    private GameManager gameManager;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        ChoosePrefab();
        InstantiatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChoosePrefab()
    {
        int monster = Random.Range(0,2);
        //monster = gameManager.monsterType;
        if (monster == 0)
            playerPrefab = avocatPrefab;
        else
            playerPrefab = fantomePrefab;
    }

    void InstantiatePlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.Log("instancié!");
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
    }
}

