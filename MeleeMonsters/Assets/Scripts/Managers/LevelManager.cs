using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;
    internal GameObject gameObjectIA;

    public SpawnPoints spawnPoints;
    internal List<Transform> spawnList;
    private GameObject spawnGameObject;

    public List<PlayerScript> players;

    public bool inSolo;

    private List<Photon.Realtime.Player> playersInGame = new List<Photon.Realtime.Player>();
    private ExitGames.Client.Photon.Hashtable _myCustomPropreties = new ExitGames.Client.Photon.Hashtable();

    // before all Start functions of all GameObject
    private void Awake()
    {
        manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
        gameManager.SetGameState(GameManager.States.Playing);
    }

    // Start is called before the first frame update
    void Start()
    {
        // to debug before 
        //GameObject canvas = GameObject.Find("Canvas");
        //GameObject pauseMenuInstance = Instantiate(gameManager.pauseMenuPrefab);
        //pauseMenuInstance.transform.SetParent(canvas.transform);
        //pauseMenuInstance.transform.localPosition = new Vector3(0f, 0f, 0f);

        gameManager.InitPlayerPrefab();

        print(PhotonNetwork.LocalPlayer.ActorNumber);

        spawnGameObject = GameObject.Find("SpawnPoints").gameObject;
        spawnPoints = spawnGameObject.GetComponent<SpawnPoints>();

        gameObjectIA = GameObject.FindGameObjectWithTag("IA").gameObject;

        if (PhotonNetwork.PlayerList.Length > 1)
        {
            gameObjectIA.SetActive(false);
            inSolo = false;
        }
        else
            inSolo = true;

        SpawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnPlayers()
    {
        int iaNbr = 0;
        if (gameObjectIA.activeSelf)
            iaNbr++;
        switch (PhotonNetwork.PlayerList.Length + iaNbr)
        {
            case 1:
                spawnList = spawnPoints.p1;
                break;
            case 2:
                spawnList = spawnPoints.p2;
                break;
            case 3:
                spawnList = spawnPoints.p3;
                break;
            default:
                spawnList = spawnPoints.p4;
                break;
        }
        print("il y a " + (PhotonNetwork.PlayerList.Length + iaNbr).ToString() + " joueurs ou ia");

        //gameManager.InstantiatePlayer(spawnList[(PhotonNetwork.LocalPlayer.ActorNumber) % spawnList.Count].position);

        gameManager.InstantiatePlayer(spawnList[(PhotonNetwork.LocalPlayer.ActorNumber) - 1].position);
        InitializePlayers();
    }

    void InitializePlayers()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            _myCustomPropreties["StillInGame"] = true;
            player.CustomProperties = _myCustomPropreties;
        }
    }

    public void PlayersStillInGame()
    {
        print("playerStillIngame est lance");
        foreach(Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if((bool)player.CustomProperties["StillInGame"])
            {
                playersInGame.Add(player);
            }
            print(player.NickName + " " + player.CustomProperties["StillInGame"].ToString());
        }
    }

    public void SearchForWinner()
    {
        PlayersStillInGame();

        if(inSolo)
        {
            if(playersInGame.Count <= 1)
            {
                if (playersInGame.Count == 1 && !(gameObjectIA.GetComponent<PlayerScript>().canStillPlay))
                    WinnerFound();
                if (playersInGame.Count == 0 && (gameObjectIA.GetComponent<PlayerScript>().canStillPlay))
                    gameManager.IAwon = true;
                EndGame();
            } 
        }
        else
        {
            if (playersInGame.Count < 2)
            {
                if (playersInGame.Count == 1)
                    WinnerFound();
                if (playersInGame.Count == 0)
                    print("vous etes tous morts");
                EndGame();
            }
        } 
    }

    void WinnerFound()
    {
        gameManager.winner = playersInGame[0];
        print(playersInGame[0].NickName + " is the winner !");
    }

    void EndGame()
    {
        PhotonNetwork.LoadLevel(6);
    }
}
