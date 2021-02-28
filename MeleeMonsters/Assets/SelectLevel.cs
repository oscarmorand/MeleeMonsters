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
        level = forest;
    }

    public void OnClickedCemetery()
    {
        level = cemetery;
    }

    public void OnClickedPlay()
    {
        PlayLevel();
    }

    void PlayLevel()
    {
        PhotonNetwork.LoadLevel(level);
    }
}
