using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;


public class SelectMonsterSoloAI : MonoBehaviour
{
    private GameManager gameManager;

    private int monsterIndex = 0;
    private GameObject playerMonsterObject;
    private GameObject AIMonsterObject;
    public GameObject aICharPanel;
    public GameObject playButton;
    public GameObject imagePlayGame;
    public GameObject tooglePlayer;
    public GameObject greenLockImage;
    public GameObject grayPanel;
    private bool monsterSelected = false;
    private bool choosingAI = false;
    public void setMonsterSelected(bool value) => monsterSelected = !value;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
        gameManager.SetGameState(GameManager.States.MonsterSelectionMenu);
        grayPanel.SetActive(true);
        tooglePlayer.SetActive(true);
        greenLockImage.SetActive(false);
        imagePlayGame.SetActive(true);
        playButton.SetActive(false);
        ChangeMonster(monsterIndex);
        ChangeAIMonster(monsterIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (!monsterSelected)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                print("right arrow key is held down");
                RightMonster();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                print("left arrow key is held down");
                LeftMonster();
            }
        }
        else
        {
            grayPanel.SetActive(false);
            tooglePlayer.SetActive(false);
            greenLockImage.SetActive(true);

            if (!choosingAI)
            {
                monsterSelected = false;
                choosingAI = true;
                aICharPanel.SetActive(true);
            }
            else
            {
                imagePlayGame.SetActive(false);
                playButton.SetActive(true);
            }
        }

    }

    public void ChangeAIMonster(int monsterIndex) // AI
    {
        if (AIMonsterObject != null)
            Destroy(AIMonsterObject);

        gameManager.SelectAIMonster(monsterIndex);
        AIMonsterObject = gameManager.InstantiateAI(new Vector3(2.8f, 0.3f, 0f));
    }

    public void ChangeMonster(int monsterIndex) // Player
    {
        if (playerMonsterObject != null)
            PhotonNetwork.Destroy(playerMonsterObject);

        gameManager.SelectMonster(monsterIndex);
        gameManager.InitPlayerPrefab();
        playerMonsterObject = gameManager.InstantiatePlayer(new Vector3(-2.8f, 0.3f, 0f));
    }
    public void LeftMonster()
    {
        monsterIndex = (monsterIndex - 1 + gameManager.monstersPrefabArray.Length) % gameManager.monstersPrefabArray.Length;
        if (choosingAI)
            ChangeAIMonster(monsterIndex);
        else
            ChangeMonster(monsterIndex);
    }

    public void RightMonster()
    {
        monsterIndex = (monsterIndex + 1) % gameManager.monstersPrefabArray.Length;
        if (choosingAI)
            ChangeAIMonster(monsterIndex);
        else
            ChangeMonster(monsterIndex);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    

    /*
    void SelectMonster(int monster)
    {
        gameManager.SelectMonster(monster);
        print(PhotonNetwork.NickName + " has choose " + monster);
    }
    */

}