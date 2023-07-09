using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayLevelManager : MonoBehaviour
{
    [SerializeField] private SmartButton _playButton;    
    [SerializeField] private SmartButton _resetButton;
    [SerializeField] private TextMeshProUGUI _editModeText;
    [SerializeField] private TextMeshProUGUI _runningText;


    public static Action OnPlayStart;
    public static Action OnReset;


    private void Awake()
    {
        _playButton._button.onClick.AddListener(OnPlayButtonPressed);
        _resetButton._button.onClick.AddListener(OnResetButtonPressed);

        ShowButton(_playButton, _editModeText);
        HideButton(_resetButton, _runningText, true);        
    }


    private void OnPlayButtonPressed()
    {
        OnPlayStart?.Invoke();

        _playButton.ClickedPunch();
        StartCoroutine(ShowHideButton(_resetButton, _playButton, _runningText, _editModeText));
    }


    private void OnResetButtonPressed()
    {
        OnReset?.Invoke();

        _resetButton.ClickedPunch();
        StartCoroutine(ShowHideButton(_playButton, _resetButton, _editModeText, _runningText));
    }

    private IEnumerator ShowHideButton(SmartButton buttonToShow, SmartButton buttonToHide, TextMeshProUGUI textToShow, TextMeshProUGUI textToHide)
    {
        HideButton(buttonToHide, textToHide);

        yield return new WaitForSeconds(0.6f);

        ShowButton(buttonToShow, textToShow);
    }

    private void ShowButton(SmartButton button, TextMeshProUGUI text)
    {
        button.Show();

        text.DOFade(1.0f, 0.3f);
    }

    private void HideButton(SmartButton button, TextMeshProUGUI text, bool instant = false)
    {
        button.Hide();

        float duration = 0.3f;
        if (instant)
        {
            duration = 0.01f;
        }
        text.DOFade(0.0f, duration);
    }

}
