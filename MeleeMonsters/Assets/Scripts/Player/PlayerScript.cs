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

    private int lives;
    public int percentage;

    internal bool isAlive = true;
    internal bool canStillPlay = true;
    internal bool isWrath = false;

    private PlayerMovement pMovement;
    private PlayerCollisions pCollisions;
    private PlayerAnimation pAnimation;
    private PlayerInputs pInputs;

    private Rigidbody2D rb;

    private List<Transform> spawnList;

    private LevelManager levelManager;
    private GameManager gameManager;

    public float reappearitionTime;

    private ExitGames.Client.Photon.Hashtable _myCustomPropreties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        pCollisions = GetComponent<PlayerCollisions>();
        pAnimation = GetComponent<PlayerAnimation>();
        pInputs = GetComponent<PlayerInputs>();

        rb = GetComponent<Rigidbody2D>();

        playerNumber = (PhotonNetwork.LocalPlayer.ActorNumber) - 1;
        nickName = PhotonNetwork.LocalPlayer.NickName;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        spawnList = levelManager.spawnList;
        levelManager.players.Add(this);

        gameManager = GameObject.Find("GameManagerPrefab").GetComponent<GameManager>();
        lives = gameManager.nbrLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterReappear()
    {
        isAlive = false;
        lives--;
        CheckStillAlive();
        if (canStillPlay)
            Invoke("Reappear", reappearitionTime);
        else
        {
            Destroy(gameObject);
            if (levelManager.inSolo)
            {
                Destroy(levelManager.gameObjectIA);
            }
        }
    }

    private void Reappear()
    {
        rb.velocity = new Vector2(0, 0);
        int randomSpawnPoint = Random.Range(0, spawnList.Count);
        transform.position = spawnList[randomSpawnPoint].position;
        //transform.position = new Vector3(0, 1, 0);
        isAlive = true;
        percentage = 0;
    }

    void CheckStillAlive()
    {
        if(lives <= 0)
        {
            canStillPlay = false;
            _myCustomPropreties["StillInGame"] = false;
            PhotonNetwork.LocalPlayer.CustomProperties = _myCustomPropreties;
            levelManager.SearchForWinner();
        }
    }
}
