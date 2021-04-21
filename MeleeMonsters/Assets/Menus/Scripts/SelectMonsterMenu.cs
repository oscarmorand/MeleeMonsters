using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;


public class SelectMonsterMenu : MonoBehaviourPun, IPunObservable
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
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (var player in gameManager.players)
        {
            playerNamesArray[i].text = player.NickName;  
            ++i;
        }


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

    public void LeftMonster()
    {
        monstersArray[monsterIndex].SetActive(false);
        monsterIndex = (monsterIndex - 1 + monstersArray.Length) % monstersArray.Length;
        monstersArray[monsterIndex].SetActive(true);
        SelectMonster(monsterIndex);
    }

    public void RightMonster()
    {
        monstersArray[monsterIndex].SetActive(false);
        monsterIndex = (monsterIndex + 1) % monstersArray.Length;
        monstersArray[monsterIndex].SetActive(true);
        SelectMonster(monsterIndex);
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            //stream.SendNext(transform.position);
            //stream.SendNext(transform.localScale); 
            //stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            //latestPos = (Vector3)stream.ReceiveNext();
            //latestScale = (Vector3)stream.ReceiveNext();
            //latestRot = (Quaternion)stream.ReceiveNext();
        }
    }

   
}
