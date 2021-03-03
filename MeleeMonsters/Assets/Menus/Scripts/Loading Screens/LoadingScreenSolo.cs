using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreenSolo : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
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
