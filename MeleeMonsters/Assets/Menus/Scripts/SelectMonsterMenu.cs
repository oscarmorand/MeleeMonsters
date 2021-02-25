using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectMonsterMenu : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickedAvocado()
    {
        SelectMonster(0);
    }

    public void OnClickedGhost()
    {
        SelectMonster(1);
    }

    void SelectMonster(int monster)
    {
        gameManager.SelectMonster(monster);
        print(PhotonNetwork.NickName + " has choose " + monster);
    }
}
