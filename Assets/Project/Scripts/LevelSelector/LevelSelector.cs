using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    [SerializeField] private Transform _buttonsHolder;
    private LevelButton[] _levelButtons;


    private void Awake()
    {
        playerAnimator.SetFloat("Speed", 1);
        enemyAnimator.SetBool("IsAttacking", true);

        SetupLevelButtons();
    }

    private void OnEnable()
    {
        if (_levelButtons != null)
        {
            for (int i = 0; _buttonsHolder.childCount > i; i++)
            {
                _levelButtons[i].OnClickedToPlayLevel += OnLevelButtonClicked;
            }
        }
    }
    private void OnDisable()
    {
        if (_levelButtons != null)
        {
            for (int i = 0; _buttonsHolder.childCount > i; i++)
            {
                _levelButtons[i].OnClickedToPlayLevel -= OnLevelButtonClicked;
            }
        }
    }


    public void Back()
    {
        AudioManager.Instance.PlaySounds("PressButton");
        playerAnimator.SetFloat("Speed", 0);
        enemyAnimator.SetBool("IsAttacking", true);
        SceneManager.LoadScene("MainMenu");
    }


    private void SetupLevelButtons()
    {
        _levelButtons = new LevelButton[_buttonsHolder.childCount];


        int lastUnlockedLevel = PlayerPrefsManager.Instance.GetLastUnlockedLevel();

        for (int i = 0; _buttonsHolder.childCount > i; i++)
        {
            _levelButtons[i] = _buttonsHolder.GetChild(i).GetComponent<LevelButton>();

            int levelNumber = i + 1;
            _levelButtons[i].Init(levelNumber);
            

            if (levelNumber <= lastUnlockedLevel)
            {
                _levelButtons[i].SetUnlockedState();

                if (PlayerPrefsManager.Instance.HasLevelGem(levelNumber))
                {
                    _levelButtons[i].ShowGem();
                }
            }
            else
            {
                _levelButtons[i].SetLockedState();
            }


            _levelButtons[i].OnClickedToPlayLevel += OnLevelButtonClicked;
        }
    }


    private void OnLevelButtonClicked(LevelButton levelButton)
    {
        PlayerPrefsManager.Instance.SetCurrentLevelNumber(levelButton.LevelNumber);
        SceneManager.LoadScene("Level" + levelButton.LevelNumber.ToString());
    }


}
