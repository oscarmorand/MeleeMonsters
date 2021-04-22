using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
