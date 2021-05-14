using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Text pointsT;
    public GameObject pauseButton;
    // Start is called before the first frame update
    public void Setup2(int score)
    {
        gameObject.SetActive(true);
        pauseButton.SetActive(false);
        pointsT.text = score.ToString() + " POINTS";
    }

    public void NextButton()
    {
        SceneManager.LoadScene("Level2");
    }

    public void NextButton2()
    {
        SceneManager.LoadScene("Level3");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
