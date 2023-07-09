using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlEnd : MonoBehaviour
{
    [SerializeField] private Collectible _levelCollectible;
    [SerializeField] private bool _isLastLevel;
    
    public Animator animator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySounds("Win");
            animator.SetBool("IsActive", true);

            if (_levelCollectible._wasCollected)
            {
                PlayerPrefsManager.Instance.SaveGemForCurrentLevel();
            }

            PlayerPrefsManager.Instance.UpdateUnlockedLevels();

            if (_isLastLevel)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }

    }
    private void setEndAnimation()
    {
        animator.SetBool("IsActive", false);
    }


}
