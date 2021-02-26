using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectMonsterMenu : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    public GameObject[] monstersArray;
    private int monsterIndex = 0;
    public TMP_Text[] playerNamesArray;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
        
        int i = 0;
        foreach(var player in gameManager.players)
        {
           playerNamesArray[i].text = player.NickName;
            ++i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            print("right arrow key is held down");
            monstersArray[monsterIndex].SetActive(false);
            monsterIndex = (monsterIndex + 1) % monstersArray.Length;
            monstersArray[monsterIndex].SetActive(true);
            SelectMonster(monsterIndex);

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("left arrow key is held down");
            monstersArray[monsterIndex].SetActive(false);
            monsterIndex = (monsterIndex - 1 + monstersArray.Length) % monstersArray.Length;
            monstersArray[monsterIndex].SetActive(true);
            SelectMonster(monsterIndex);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /*
    public void OnClickedAvocado()
    {
        SelectMonster(0);
    }

    public void OnClickedGhost()
    {
        SelectMonster(1);
    }
    */
    void SelectMonster(int monster)
    {
        gameManager.SelectMonster(monster);
        print(PhotonNetwork.NickName + " has choose " + monster);
    }
}
