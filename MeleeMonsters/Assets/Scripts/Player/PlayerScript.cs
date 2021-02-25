using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScript : MonoBehaviour
{
    public enum Monsters
    {
        Avocado,
        Ghost,
        Yeti,
        Kraken,
    }

    public string nickName;
    private int playerNumber;

    public Monsters monster;

    public int lives;
    public int percentage;

    public bool isAlive;
    public bool isInGame;
    public bool isWrath;

    private PlayerMovement pMovement;
    private PlayerCollisions pCollisions;
    private PlayerAnimation pAnimation;
    private PlayerInputs pInputs;

    private SpawnPoints spawnPoints;
    private List<Transform> spawnList;

    private LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        pCollisions = GetComponent<PlayerCollisions>();
        pAnimation = GetComponent<PlayerAnimation>();
        pInputs = GetComponent<PlayerInputs>();

        spawnPoints = GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>();

        playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        nickName = PhotonNetwork.LocalPlayer.NickName;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        spawnList = levelManager.spawnList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reappear()
    {
        lives--;
        CheckStillAlive();
        transform.position = spawnList[playerNumber].position;
    }

    void CheckStillAlive()
    {
        if(lives <= 0)
        {
            isAlive = false;
        }
    }
}
