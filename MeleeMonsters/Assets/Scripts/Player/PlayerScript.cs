﻿using System.Collections;
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
    public float maxWrathTime;
    internal float wrathTime;
    internal float wrathPercentage = 100;

    private PlayerMovement pMovement;
    private PlayerCollisions pCollisions;
    private PlayerAnimation pAnimation;
    private PlayerInputs pInputs;

    private PhotonView photonView;

    private Rigidbody2D rb;

    private List<Transform> spawnList;

    private LevelManager levelManager;
    private GameManager gameManager;

    public float reappearitionTime;

    private ExitGames.Client.Photon.Hashtable _myCustomPropreties = new ExitGames.Client.Photon.Hashtable();

    internal GameObject body;
    internal SpriteRenderer bodySprite;

    // Start is called before the first frame update
    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        pCollisions = GetComponent<PlayerCollisions>();
        pAnimation = GetComponent<PlayerAnimation>();
        pInputs = GetComponent<PlayerInputs>();

        photonView = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody2D>();

        playerNumber = (PhotonNetwork.LocalPlayer.ActorNumber) - 1;

        if (gameObject.tag == "IA")
            nickName = "IA";
        else
            nickName = PhotonNetwork.LocalPlayer.NickName;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        spawnList = levelManager.spawnList;
        levelManager.players.Add(this);

        gameManager = GameObject.Find("GameManagerPrefab").GetComponent<GameManager>();
        lives = gameManager.nbrLives;

        body = this.transform.Find("Body").gameObject;
        bodySprite = body.GetComponent<SpriteRenderer>();

        wrathTime = maxWrathTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWrath)
            CheckWrathState();
    }

    public void WrathModeState()
    {
        isWrath = true;
        bodySprite.color = Color.red;
    }

    public void CheckWrathState()
    {
        wrathTime -= Time.deltaTime;
        wrathPercentage = (wrathTime / maxWrathTime) * 100;
        if(wrathTime <= 0)
        {
            isWrath = false;
            wrathTime = maxWrathTime;
            wrathPercentage = 100;
            bodySprite.color = Color.white;
        }
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

    public void TakeDamage(int damage)
    {
        if(photonView.IsMine)
        {
            percentage += damage;
            print("aie " + nickName + " a pris une attaque d'une puissance de " + damage + " pourcents et monte maintenant à " + percentage + " pourcents!");
        }
    }
}
