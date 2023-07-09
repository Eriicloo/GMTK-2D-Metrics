using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("MainMenu");
    }
    public void PlayGame()
    {


    }

    public void Options()
    {

    }

    public void Credits()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
