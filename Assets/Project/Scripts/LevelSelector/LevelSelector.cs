using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;
    private void Start()
    {
        playerAnimator.SetFloat("Speed", 1);
        enemyAnimator.SetBool("IsAttacking", true);
    }


    public void Back()
    {
        AudioManager.Instance.PlaySounds("PressButton");
        playerAnimator.SetFloat("Speed", 0);
        enemyAnimator.SetBool("IsAttacking", true);
        SceneManager.LoadScene("MainMenu");

    }
}
