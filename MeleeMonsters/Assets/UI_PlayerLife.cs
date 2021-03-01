using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PlayerLife : MonoBehaviour
{
    [SerializeField]
    private TMP_Text UI_percentage;

    [SerializeField]
    private TMP_Text username;

    private GameManager gameManager;
    private LevelManager levelManager;

    public int playerIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager?.GetComponent<GameManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        username.text = gameManager.players[playerIndex].NickName;
    }

    // Update is called once per frame
    void Update()
    {
        UI_percentage.text = levelManager.players[playerIndex].percentage.ToString();
    }
}
