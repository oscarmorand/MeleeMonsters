using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreenSolo : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.OfflineMode = true;
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        GameManager gameManager = manager.GetComponent<GameManager>();
        gameManager.SetGameState(GameManager.States.MonsterSelectionMenu);
        SceneManager.LoadScene("SelectMonster");
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.OfflineMode = true;

        base.OnConnectedToMaster();
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 1;
            options.IsVisible = false;
            PhotonNetwork.JoinOrCreateRoom("solo", options, TypedLobby.Default);
            SceneManager.LoadScene("SelectMonster");
        }
          
    }
}
