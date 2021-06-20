using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [System.Serializable]
    public class ScreenResolution
    {
        public int width;
        public int height;
    }

    public ScreenResolution[] resolutions;

    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public GameObject gameOptionsPanel;
    public GameObject goldImage;
    public Slider livesSlider;
    public TMP_Text numberText;

    private GameManager gameManager;

    
    private void Start()
    {

    }

    public void SetResolution(int resolutionIndex)
    {
        // Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    /*
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    */

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetGameOptionPanel()
    {
        goldImage.SetActive(true);
        gameOptionsPanel.SetActive(true);
    }

    public void SetNumberLives()
    {
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();

        gameManager.nbrLives = (int) livesSlider.value;
        numberText.text = gameManager.nbrLives.ToString();
    }
}
