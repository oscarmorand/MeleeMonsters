using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectLevel : MonoBehaviour
{

    string level;
    private GameManager gameManager;

    // UI
    public GameObject gameOptionsPanel;
    public GameObject goldImage;
    public Slider livesSlider;
    public TMP_Text numberText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();
        livesSlider.value = gameManager.nbrLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickedForest()
    {
        if(PhotonNetwork.IsMasterClient)
            level = "Forest";
    }

    public void OnClickedCemetery()
    {
        if (PhotonNetwork.IsMasterClient)
            level = "Cemetery";
    }

    public void OnClickedAbyss()
    {
        if (PhotonNetwork.IsMasterClient)
            level = "Abyss";
    }

    public void OnClickedFrozenCave()
    {
        if (PhotonNetwork.IsMasterClient)
            level = "Grotte";
    }


    public void OnClickedPlay()
    {
        if (PhotonNetwork.IsMasterClient)
            PlayLevel();
    }

    void PlayLevel()
    {
        gameManager.SetGameState(GameManager.States.EnteringLevel);
        PhotonNetwork.LoadLevel(level);
    }

    // UI
    public void SetGameOptionPanel()
    {
        goldImage.SetActive(true);
        gameOptionsPanel.SetActive(true);
    }

    public void SetNumberLives()
    {
        gameManager.nbrLives = (int)livesSlider.value;
        numberText.text = gameManager.nbrLives.ToString();
    }
}
