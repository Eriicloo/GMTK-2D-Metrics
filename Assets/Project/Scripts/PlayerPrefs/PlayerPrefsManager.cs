using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager Instance;

    public const string GEM_PREFIX = "gemLevel";
    public const string UNLOCKED_LEVELS = "unlockedLevels";

    int _currentLevelNumber;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (!PlayerPrefs.HasKey(UNLOCKED_LEVELS))
        {
            PlayerPrefs.SetInt(UNLOCKED_LEVELS, 1);
        }
    }



    public void SetCurrentLevelNumber(int levelNumber)
    {
        _currentLevelNumber = levelNumber;
    }


    public bool HasLevelGem(int levelNumber)
    {
        string gemPref = GEM_PREFIX + levelNumber.ToString();
        if (PlayerPrefs.HasKey(gemPref))
        {
            return PlayerPrefs.GetInt(gemPref) == 1;
        }
        else
        {
            PlayerPrefs.SetInt(gemPref, 0);
            PlayerPrefs.Save();
            return false;
        }
    }

    public void SaveGemForCurrentLevel()
    {
        string gemPref = GEM_PREFIX + _currentLevelNumber.ToString();
        PlayerPrefs.SetInt(gemPref, 1);
        PlayerPrefs.Save();
    }
    

    public void UpdateUnlockedLevels()
    {
        int lastUnlockedLevel = GetLastUnlockedLevel();
        if (lastUnlockedLevel == _currentLevelNumber)
        {
            PlayerPrefs.SetInt(UNLOCKED_LEVELS, lastUnlockedLevel + 1);
            PlayerPrefs.Save();
        }
    }

    public int GetLastUnlockedLevel()
    {
        return PlayerPrefs.GetInt(UNLOCKED_LEVELS);
    }


}
