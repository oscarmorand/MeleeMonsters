using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;


public class SelectMonsterMenu : MonoBehaviourPun, IPunObservable
{
    private GameManager gameManager;

    private int monsterIndex = 0;
    public TMP_Text[] playerNamesArray;
    private GameObject playerMonsterObject;
    public GameObject[] selectCharPanel;
    private bool monsterSelected = false;
    public void setMonsterSelected(bool value) => monsterSelected = value;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
        gameManager.SetGameState(GameManager.States.MonsterSelectionMenu);
        ChangeMonster(monsterIndex);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in gameManager.playersList)
        {
            selectCharPanel[player.ActorNumber-1].SetActive(true);
            playerNamesArray[player.ActorNumber-1].text = player.NickName;  
        }

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
        
        
    }

    public void ChangeMonster(int monsterIndex)
    {
        if (playerMonsterObject != null)
            PhotonNetwork.Destroy(playerMonsterObject);

        gameManager.SelectMonster(monsterIndex);
        gameManager.InitPlayerPrefab();
        playerMonsterObject = gameManager.InstantiatePlayer(new Vector3((PhotonNetwork.LocalPlayer.ActorNumber - 1) * 2.66f - 4.0f, -0.2f, 0.0f));
    }
    public void LeftMonster()
    {
        monsterIndex = (monsterIndex - 1 + gameManager.monstersPrefabArray.Length) % gameManager.monstersPrefabArray.Length;
        ChangeMonster(monsterIndex);
    }

    public void RightMonster()
    {
        monsterIndex = (monsterIndex + 1) % gameManager.monstersPrefabArray.Length;
        ChangeMonster(monsterIndex);
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
