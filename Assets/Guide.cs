using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Guide : MonoBehaviour
{
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
