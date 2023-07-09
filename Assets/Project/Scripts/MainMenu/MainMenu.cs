using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    private void Start()
    {
        if (!AudioManager.Instance.isPlayingMainMenuMusic)
        {
            AudioManager.Instance.PlayMusic("MainMenu");
        }
    }
    public void PlayGame()
    {
        AudioManager.Instance.PlaySounds("PressButton");
        SceneManager.LoadScene("LevelSelector");
    }

    public void Credits()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
