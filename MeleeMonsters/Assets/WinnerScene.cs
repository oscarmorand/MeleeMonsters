using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerScene : MonoBehaviour
{
    [SerializeField]
    private TMP_Text UIWinner;
    private string winner;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager?.GetComponent<GameManager>();


        if (gameManager.IAwon)
        {
            winner = "The AI";
            //gameManager.InstantiateAI(new Vector3(0f, -1.8f, 0));
        }
        else
        {
            winner = gameManager.winner.NickName;
            //gameManager.InstantiatePlayer(new Vector3(0f, -1.8f, 0));
        }  
        UIWinner.text = winner + " is the winner of the game !";

        //gameManager.InstantiatePlayer(new Vector3(0f, -2, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
