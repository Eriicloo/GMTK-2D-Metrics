using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayLevelManager : MonoBehaviour
{
    [SerializeField] private SmartButton _playButton;    
    [SerializeField] private SmartButton _resetButton;


    public static Action OnPlayStart;
    public static Action OnReset;


    private void Awake()
    {
        _playButton._button.onClick.AddListener(OnPlayButtonPressed);
        _resetButton._button.onClick.AddListener(OnResetButtonPressed);

        ShowButton(_playButton);
        HideButton(_resetButton);        
    }


    private void OnPlayButtonPressed()
    {
        OnPlayStart?.Invoke();

        _playButton.ClickedPunch();
        StartCoroutine(ShowHideButton(_resetButton, _playButton));
    }


    private void OnResetButtonPressed()
    {
        OnReset?.Invoke();

        _resetButton.ClickedPunch();
        StartCoroutine(ShowHideButton(_playButton, _resetButton));

        //HideButton(_resetButton);
        //ShowButton(_playButton);
    }

    private IEnumerator ShowHideButton(SmartButton buttonToShow, SmartButton buttonToHide)
    {
        HideButton(buttonToHide);

        yield return new WaitForSeconds(0.6f);

        ShowButton(buttonToShow);
    }

    private void ShowButton(SmartButton button)
    {
        button.Show();
    }

    private void HideButton(SmartButton button)
    {
        button.Hide();
    }

}
