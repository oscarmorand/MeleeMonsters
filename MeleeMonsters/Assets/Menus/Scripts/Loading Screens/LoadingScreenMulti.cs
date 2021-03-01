using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreenMulti : MonoBehaviourPunCallbacks
{
   public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        if (PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Rooms");
        }
    }
}
