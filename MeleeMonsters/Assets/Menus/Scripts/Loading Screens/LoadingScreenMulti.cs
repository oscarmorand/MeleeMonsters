using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreenMulti : MonoBehaviourPunCallbacks
{
    public void Start()
    {
    
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.OfflineMode = false;

        base.OnConnectedToMaster();
        if (PhotonNetwork.IsConnected)
        {
            GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
            GameManager gameManager = manager.GetComponent<GameManager>();
            gameManager.SetGameState(GameManager.States.RoomSelectionMenu);
            SceneManager.LoadScene("Rooms");
        }
    }
}
