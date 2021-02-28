using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    public SpawnPoints spawnPoints;
    internal List<Transform> spawnList;
    private GameObject spawnGameObject;

    public List<PlayerScript> players;

    private List<Photon.Realtime.Player> playersInGame = new List<Photon.Realtime.Player>();
    private ExitGames.Client.Photon.Hashtable _myCustomPropreties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();

        int mNumber = gameManager.monsterNbr;
        gameManager.InitPlayerPrefab(mNumber);

        print(PhotonNetwork.LocalPlayer.ActorNumber);

        spawnGameObject = GameObject.Find("SpawnPoints").gameObject;
        spawnPoints = spawnGameObject.GetComponent<SpawnPoints>();

        SpawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnPlayers()
    {
        switch (PhotonNetwork.PlayerList.Length)
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
        if (playersInGame.Count < 2)
        {
            if (playersInGame.Count == 1)
                WinnerFound();
            if (playersInGame.Count == 0)
                print("beurk vous etes nul");
            EndGame();
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
