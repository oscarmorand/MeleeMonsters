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
    private Image monsterImageComponent;
    private Slider slider;

    public int playerIndex = -1;


    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        panel = transform.Find("Panel PlayerLife").gameObject;
        GameObject selectedChracter = panel.transform.Find("SelectedCharacter").gameObject; 
        monsterImageComponent = selectedChracter.GetComponent<Image>();

        GameObject wrathModeBar = panel.transform.Find("WrathModeBar").gameObject;
        slider = wrathModeBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIndex < levelManager.playersScripts.Count)
        {
            panel.SetActive(true);

            PlayerScript playerScript =  levelManager.playersScripts[playerIndex];

            username.text = playerScript.nickName;
            slider.value = playerScript.wrathPercentage;
            UI_percentage.text = playerScript.percentage.ToString(); 
            monsterImageComponent.sprite = gameManager.monstersSpriteArray[playerScript.monsterIndex];
        }
        else
        {
            panel.SetActive(false);
        }
           
           
    }
}
