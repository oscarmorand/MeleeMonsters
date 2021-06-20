using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class WinnerScene : MonoBehaviour
{
    [SerializeField]
    private TMP_Text UIWinner;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager?.GetComponent<GameManager>();
        /*
        if (gameManager.IAwon)
        {
            gameManager.InstantiateAI(new Vector3(0f, -1.8f, 0));
        }
        else
        {
            gameManager.InstantiatePlayer(new Vector3(0f, -1.8f, 0));
        }
        */
        if (gameManager.winner == "")
            UIWinner.text = "No monster is victorious !";
        else
            UIWinner.text = gameManager.winner + " is the winner of the game !";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replay()
    {
        PhotonNetwork.DestroyAll();
        PhotonNetwork.LeaveRoom(true);
        PhotonNetwork.Disconnect();
        gameManager.nbrLives = 3;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }


}
