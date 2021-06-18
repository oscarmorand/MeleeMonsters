using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    internal GameObject gameObjectIA;

    public SpawnPoints spawnPoints;
    internal List<Transform> spawnList;
    private GameObject spawnGameObject;

    public List<PlayerScript> playersScripts;

    public bool inSolo;

    //private List<Photon.Realtime.Player> playersInGame = new List<Photon.Realtime.Player>();
    private ExitGames.Client.Photon.Hashtable _myCustomPropreties = new ExitGames.Client.Photon.Hashtable();

    // before all Start functions of all GameObject
    private void Awake()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
        gameManager.SetGameState(GameManager.States.Start321GO);
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
        
        inSolo = PhotonNetwork.OfflineMode;

        SpawnPlayers();

        if (inSolo)
        {
            //if (playersScripts.Count == 1)
            gameObjectIA = gameManager.InstantiateAI(spawnPoints.p2[1].position);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetGameState() == GameManager.States.Playing)
        { 
            SearchForWinner(); 
        }
            
    }

    void SpawnPlayers()
    {
        int iaNbr = 0;
        if (inSolo)
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

    /*
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
    */
    public void SearchForWinner()
    {
        //PlayersStillInGame();

        if(inSolo)
        {
            if (playersScripts[0].lives <= 0)
            {
                // AI won
                gameManager.IAwon = true;
                gameManager.winner = playersScripts[1].nickName;
                print(playersScripts[1].nickName + " is the winner !");
                EndGame();
            }
            else if (playersScripts[1].lives <= 0)
            {
                // Player won
                gameManager.winner = playersScripts[0].nickName;
                print(playersScripts[0].nickName + " is the winner !");
                EndGame();
            }
            /*
            if(playersInGame.Count <= 1)
            {
                if (playersInGame.Count == 1 && !(gameObjectIA.GetComponent<PlayerScript>().canStillPlay))
                    WinnerFound();
                //if (playersInGame.Count == 0 && (gameObjectIA.GetComponent<PlayerScript>().canStillPlay))
                   //gameManager.IAwon = true;
                EndGame();
            } */
        }
        else
        {
            int count = 0;
            string winnerPlayerScript = "";
            foreach (PlayerScript player in playersScripts)
            {
                if (player != null)
                {
                    if (player.lives > 0)
                    {
                        ++count;
                        winnerPlayerScript = player.nickName;
                    }
                }
                    
            }

            if (count < 2)
            {
                print("GoToEndGame");
                if (count == 1)
                {
                    gameManager.winner = winnerPlayerScript;
                    print(winnerPlayerScript + " is the winner !");
                }
                    
                if (count == 0)
                    print("vous etes tous morts");

                EndGame();
            }
        } 
    }

    void EndGame()
    {
        print("EndGame");
        gameManager.SetGameState(GameManager.States.End);
        SceneManager.LoadScene("WinnerScene");
    }
}
