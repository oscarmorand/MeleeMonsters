using UnityEngine;

public class GamePauseMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;

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
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void OptionMenu()
    {
        optionsPanel.SetActive(true);
    }

    public void BackPauseMenu()
    {
        optionsPanel.SetActive(false);
    }
}
