using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlEnd : MonoBehaviour
{
    [SerializeField] private Collectible _levelCollectible;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySounds("Win");

            if (_levelCollectible._wasCollected)
            {
                PlayerPrefsManager.Instance.SaveGemForCurrentLevel();
            }

            PlayerPrefsManager.Instance.UpdateUnlockedLevels();


            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }




}
