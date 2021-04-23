using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerLife : MonoBehaviour
{
    [SerializeField]
    private TMP_Text UI_percentage;

    [SerializeField]
    private TMP_Text username;

    private GameManager gameManager;
    private LevelManager levelManager;

    private GameObject panel;
    public Slider slider;

    public int playerIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager?.GetComponent<GameManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        panel = transform.Find("Panel PlayerLife").gameObject;
        slider.maxValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIndex < gameManager.players.Count)
        {
            username.text = gameManager.players[playerIndex].NickName;
            slider.value = levelManager.players[playerIndex].wrathPercentage;
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
           

        if (playerIndex < levelManager.players.Count)
        {
            UI_percentage.text = levelManager.players[playerIndex].percentage.ToString();
        }
            
    }
}
