using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public GameObject PauseScreen;
    public GameObject PauseButton;

    bool GamePaused;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}