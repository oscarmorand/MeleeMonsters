using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    public SpawnPoints spawnPoints;
    public List<Transform> spawnList;

    public List<PlayerScript> players;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();

        int mNumber = gameManager.monsterNbr;
        gameManager.InitPlayerPrefab(mNumber);

        print(PhotonNetwork.LocalPlayer.ActorNumber);

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
    }
}
