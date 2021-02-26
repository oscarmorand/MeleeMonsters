using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    public SpawnPoints spawnPoints;
    private List<Transform> spawnList;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();

        gameManager.InitPlayerPrefab();

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
            default:
                spawnList = spawnPoints.p2;
                break;
        }
        gameManager.InstantiatePlayer(spawnList[(PhotonNetwork.LocalPlayer.ActorNumber) - 1].position);
    }
}
