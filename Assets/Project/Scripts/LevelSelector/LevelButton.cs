using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _locked;
    [SerializeField] private GameObject _gemHolder;
    [SerializeField] private GameObject _gem;
    [SerializeField] private TextMeshProUGUI _levelNumberText;

    private int _levelNumber;
    public int LevelNumber => _levelNumber;
    private bool _isLocked = false;


    public Action<LevelButton> OnClickedToPlayLevel;


    private void Awake()
    {
        _levelNumberText.text = _levelNumber.ToString();

        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnValidate()
    {
        _levelNumberText.text = _levelNumber.ToString();
    }

    public void Init(int levelNumber)
    {
        _levelNumber = levelNumber;
        _levelNumberText.text = _levelNumber.ToString();
    }

    public void SetLockedState()
    {
        _isLocked = true;

        _locked.SetActive(true);
        _gemHolder.SetActive(false);
        _gem.SetActive(false);
        _levelNumberText.gameObject.SetActive(false);
    }

    public void SetUnlockedState()
    {
        _isLocked = false;

        _locked.SetActive(false);
        _gemHolder.SetActive(true);
        _gem.SetActive(false);
        _levelNumberText.gameObject.SetActive(true);
    }

    public void ShowGem()
    {
        _gem.SetActive(true);
    }

    private void OnButtonClicked()
    {
        if (_isLocked)
        {

        }
        else
        {
            OnClickedToPlayLevel?.Invoke(this);
        }
    }

}
