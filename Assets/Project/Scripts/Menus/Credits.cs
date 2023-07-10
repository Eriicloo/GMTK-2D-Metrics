using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    private void Start()
    {
        playerAnimator.SetBool("IsAttacking", true);

        if (!AudioManager.Instance.isPlayingMainMenuMusic)
        {
            AudioManager.Instance.PlayMusic("MainMenu");
        }
    }

    public void Back()
    {
        AudioManager.Instance.PlaySounds("PressButton");
        playerAnimator.SetFloat("Speed", 0);
        enemyAnimator.SetBool("IsAttacking", false);
        SceneManager.LoadScene("MainMenu");
    }
}
