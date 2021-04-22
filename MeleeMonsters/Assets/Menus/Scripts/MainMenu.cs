using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public void PlaySolo()
    {
        SceneManager.LoadScene("LoadingScreenSolo"); //Loading screen solo
    }

    public void PlayMulti()
    {
        SceneManager.LoadScene("LoadingScreenMultiplayer");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void OptionMenu()
    {
        optionsPanel.SetActive(true);
    }

    public void BackMainMenu()
    {
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
