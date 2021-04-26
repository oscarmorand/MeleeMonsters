using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections;

public class GamePauseMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject pauseMenuUI;
    private LevelManager levelManager;

    private bool soloMode;
    public static bool gameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        soloMode = levelManager.inSolo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);

        if (soloMode)
            Time.timeScale = 0;

        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
        gameIsPaused = false;
    }

    public void OptionMenu()
    {
        optionsPanel.SetActive(true);
    }

    public void BackOptionMenu()
    {
        optionsPanel.SetActive(false);
    }

    public void BackMainMenu()
    {
        DisconnectPlayer();
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene("Menu");
    }
}
