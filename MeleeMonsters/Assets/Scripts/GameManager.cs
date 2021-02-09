using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject avocat;
    public GameObject fantome;

    public int monsterNbr;

    private GameObject playerPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.monsterNbr = Random.Range(0, 2);
        print("my monster has type" + this.monsterNbr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiatePlayer ()
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

    public void InitPlayerPrefab(int monsterNbr)
    {
        if (monsterNbr==0)
        {
            playerPrefab = avocat;
        }
        else
        {
            playerPrefab = fantome;
        }
    }
}
