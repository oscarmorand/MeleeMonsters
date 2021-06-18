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
    public GameObject[] monstersPrefabArray;
    public GameObject[] aImonstersArray;
    public Sprite[] monstersSpriteArray;


    private int localPlayerMonsterIndex = 0;
    private int AIMonsterIndex = 0;

    private GameObject playerPrefab;
    public GameObject AIPrefab;

    public int nbrLives;

    public List<Player> playersList = new List<Player>(); // players in current room

    //internal Photon.Realtime.Player winner;
    internal PlayerScript winner;
    internal bool IAwon = false;

    public enum States
    {
        MainMenu,
        RoomSelectionMenu,
        MonsterSelectionMenu,
        EnteringLevel,
        Start321GO,
        Playing,
        End
    }

    private States currentGameState = States.MainMenu;
    public void SetGameState(States newState) => currentGameState = newState;
    public States GetGameState() => currentGameState;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.monsterNbr = Random.Range(0, 2);
        this.localPlayerMonsterIndex = 0;

        print("my monster has type " + this.localPlayerMonsterIndex + " by default");
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 60000; // in milliseconds. any high value for debug
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject InstantiatePlayer (Vector3 pos)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.Log("instancié!");
            object[] data = new object[]
            {
                localPlayerMonsterIndex  // to know which monster we instanciate for replica
            };
            return PhotonNetwork.Instantiate(this.playerPrefab.name, pos, Quaternion.identity, 0, data);
        }
        return null;
    }

    public GameObject InstantiateAI (Vector3 pos)
    {
        if (AIPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.Log("instancié!");
            object[] data = new object[]
            {
                AIMonsterIndex  // to know which monster we instanciate for replica
            };
            return PhotonNetwork.Instantiate(AIPrefab.name, pos, Quaternion.identity, 0, data);
        }
        return null;
    }

    public void SelectMonster(int _monsterNbr)
    {
        localPlayerMonsterIndex = _monsterNbr;
    }

    public void InitPlayerPrefab()
    {
        playerPrefab = monstersPrefabArray[localPlayerMonsterIndex];
    }

    public void SelectAIMonster(int _AImonsterNbr)
    {
        AIMonsterIndex = _AImonsterNbr;
        AIPrefab = aImonstersArray[AIMonsterIndex];
    }

    public override void OnJoinedRoom()
    {
        InitPlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        InitPlayerList(); 
    }

    private void InitPlayerList()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        playersList.Clear(); 
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            playersList.Add(playerInfo.Value);
        }

    }
}
