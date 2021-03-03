using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{

    private RoomsCanvases _roomCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
