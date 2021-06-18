using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{

    int level;

    public int forest;
    public int cemetery;
    public int abyss;
    public int frozenCave;

    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickedForest()
    {
        if(PhotonNetwork.IsMasterClient)
            level = forest;
    }

    public void OnClickedCemetery()
    {
        if (PhotonNetwork.IsMasterClient)
            level = cemetery;
    }

    public void OnClickedAbyss()
    {
        if (PhotonNetwork.IsMasterClient)
            level = abyss;
    }

    public void OnClickedFrozenCave()
    {
        if (PhotonNetwork.IsMasterClient)
            level = frozenCave;
    }


    public void OnClickedPlay()
    {
        if (PhotonNetwork.IsMasterClient)
            PlayLevel();
    }

    void PlayLevel()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        GameManager gameManager = manager.GetComponent<GameManager>();
        gameManager.SetGameState(GameManager.States.EnteringLevel);
        PhotonNetwork.LoadLevel(level);
    }
}
