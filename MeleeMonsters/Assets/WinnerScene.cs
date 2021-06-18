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
            gameManager.InstantiateAI(new Vector3(0f, -1.8f, 0));
        }
        else
        {
            gameManager.InstantiatePlayer(new Vector3(0f, -1.8f, 0));
        }
        winner = gameManager.winner.nickName;
        UIWinner.text = winner + " is the winner of the game !";

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
