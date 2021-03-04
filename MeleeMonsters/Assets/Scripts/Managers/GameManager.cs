﻿using System.Collections;
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
    public GameObject[] monstersArray;

    private int localPlayerMonsterIndex = 0;

    private GameObject playerPrefab;

    public int nbrLives;

    public List<Player> players = new List<Player>(); // players in current room

    internal Photon.Realtime.Player winner;


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

    /*public void InstantiatePlayer (Vector3 pos)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.Log("instancié!");
            PhotonNetwork.Instantiate(this.playerPrefab.name, pos, Quaternion.identity, 0);
        }
    }*/

    public void InstantiatePlayer(Vector3 pos, bool Solo)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (Solo)
            {
                GameObject Prefab = (GameObject)Resources.Load(this.playerPrefab.name, typeof(GameObject));
                Instantiate(Prefab, pos, Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, pos, Quaternion.identity, 0);
            }

            Debug.Log("instancié!");
        }
    }

    public void SelectMonster(int _monsterNbr)
    {
        localPlayerMonsterIndex = _monsterNbr;
    }

    public void InitPlayerPrefab()
    {
        playerPrefab = monstersArray[localPlayerMonsterIndex];
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
        players.Clear(); 
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            players.Add(playerInfo.Value);
        }

    }
}
