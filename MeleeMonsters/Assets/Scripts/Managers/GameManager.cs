using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Local Player")]
    public string playerName;
    public void SetPlayerName(string _value)   // property
    {
        playerName = _value;
        PhotonNetwork.NickName = playerName;
    }

    [Header("Monsters")]
    public GameObject avocado;
    public GameObject ghost;
    public GameObject yeti;
    public GameObject kraken;

    public int monsterNbr;

    private GameObject playerPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.monsterNbr = Random.Range(0, 2);
        this.monsterNbr = 0;

        print("my monster has type " + this.monsterNbr + " by default");
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

    public void SelectMonster(int _monsterNbr)
    {
        monsterNbr = _monsterNbr;
    }

    public void InitPlayerPrefab(int monsterNbr)
    {
        switch(monsterNbr)
        {
            case 0:
                playerPrefab = avocado;
                break;
            case 1:
                playerPrefab = ghost;
                break;
            case 2:
                playerPrefab = yeti;
                break;
            default:
                playerPrefab = kraken;
                break;
        }
    }
}
