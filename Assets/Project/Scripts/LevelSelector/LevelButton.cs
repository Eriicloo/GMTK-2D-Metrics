using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private GameObject _locked;
    [SerializeField] private GameObject _gemHolder;
    [SerializeField] private GameObject _gem;
    [SerializeField] private TextMeshProUGUI _levelNumberText;

    [SerializeField] private string _levelNumber;

    private void Awake()
    {
        _levelNumberText.text = _levelNumber;
    }

    private void OnValidate()
    {
        _levelNumberText.text = _levelNumber;
    }

    public void SetLockedState()
    {
        _locked.SetActive(true);
        _gemHolder.SetActive(false);
        _gem.SetActive(false);
        _levelNumberText.gameObject.SetActive(false);
    }

    public void SetUnlockedState()
    {
        _locked.SetActive(false);
        _gemHolder.SetActive(true);
        _gem.SetActive(false);
        _levelNumberText.gameObject.SetActive(true);
    }

    public void ShowGem()
    {
        _gem.SetActive(true);
    }

}
