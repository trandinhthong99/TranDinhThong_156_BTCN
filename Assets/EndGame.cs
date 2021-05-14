using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject pauseButton;
    // Start is called before the first frame update
    public void Setup()
    {
        gameObject.SetActive(true);
        pauseButton.SetActive(false);
        //Time.timeScale = 0;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
