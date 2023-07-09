using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MetricsInteractableManager : MonoBehaviour
{
    [Header("INTERACTABLES")]
    [SerializeField] private MetricsInteractable[] interactables;
    [SerializeField] public Color _defaultInteractableOutline = Color.cyan;
    [SerializeField] public Color _activeInteractableOutline = Color.yellow;

    private MetricsInteractable _currentActiveInteractable;
    private MetricsInteractable _activeInteractableBeforePlay;

    [Header("POINTS")]
    [SerializeField] private int _maxPoints = 3;
    [HideInInspector] private int _currentPoints;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _maxPointsText;
    private Vector3 _pointsTextLocalPosition;
    [SerializeField] private Image _underlineImage;
    [SerializeField] public Color _defaultPointsColor = Color.white;
    [SerializeField] public Color _pointsGainedColor = Color.green;
    [SerializeField] public Color _notEnoughPointsColor = Color.red;

    [Header("BUTTONS")]
    [SerializeField] private SmartButton _resetPointsButton;

    [Header("OPTIONS MENU")]
    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private SmartButton _closeButton;
    [SerializeField] private Button _confirmCloseButton;
    [SerializeField] private Button _backToGameButton;

    private bool _isInEditMode;
    private bool _isInSubMenu;

    public static Action OnPointsReset;


    private void Awake()
    {
        foreach (var interactable in interactables)
        {
            interactable.Init(this);                      
        }

        _isInSubMenu = false;

        _currentActiveInteractable = null;
        _activeInteractableBeforePlay = null;


        _maxPointsText.text = "/" + _maxPoints.ToString();

        _currentPoints = _maxPoints;        
        UpdatePointsText();
        _pointsText.color = _defaultPointsColor;
        _underlineImage.color = _defaultInteractableOutline;

        _pointsTextLocalPosition = _pointsText.rectTransform.localPosition;

        EnableMetricsEditMode();

        _resetPointsButton._button.onClick.AddListener(OnResetPointsButtonPressed);


        _closeButton._button.onClick.AddListener(OnCloseButtonClicked);
        _confirmCloseButton.onClick.AddListener(OnConfirmCloseButtonClicked);
        _backToGameButton.onClick.AddListener(OnBackToGameButtonClicked);

        HideOptionsCanvas();
    }

    private void OnEnable()
    {
        PlayLevelManager.OnPlayStart += DisableMetricsEditMode;
        PlayLevelManager.OnReset += EnableMetricsEditMode;
    }
    private void OnDisable()
    {
        PlayLevelManager.OnPlayStart -= DisableMetricsEditMode;
        PlayLevelManager.OnReset -= EnableMetricsEditMode;
    }


    public bool CanDisplay()
    {
        return _isInEditMode && !_isInSubMenu;
    }

    private void EnableMetricsEditMode()
    {
        _isInEditMode = true;
        _resetPointsButton.Hide();

        if (_activeInteractableBeforePlay != null)
        {            
            DisplayMetricsInteractable(_activeInteractableBeforePlay);
            _activeInteractableBeforePlay = null;
        }

        PointsTextFloatUp();
    }
    private void DisableMetricsEditMode()
    {
        _isInEditMode = false;

        if (_currentPoints != _maxPoints)
        {
            _resetPointsButton.Show();
        }        

        if (_currentActiveInteractable != null)
        {
            _activeInteractableBeforePlay = _currentActiveInteractable;

            HideCurrentInteractableMetrics();
        }

        PointstextStopFloat();
    }


    public void DisplayMetricsInteractable(MetricsInteractable interactable)
    {
        if (_currentActiveInteractable != null && _currentActiveInteractable != interactable)
        {
            HideCurrentInteractableMetrics();
        }

        _currentActiveInteractable = interactable;

        interactable.StartDisplaying();
    }

    public void HideCurrentInteractableMetrics()
    {
        _currentActiveInteractable.StopDisplaying();
        _currentActiveInteractable = null;
    }



    public bool OnIncrementMetricButtonPressed(Metric metric)
    {
        bool couldEffectuate = true;
        bool isSpendingPoints = metric._value >= metric._defaultValue;

        if (isSpendingPoints)
        {
            if (metric._cost <= _currentPoints)
            {
                metric.Increment();

                _currentPoints -= metric._cost;

                PointsSpentAnimation();
            }
            else
            {
                couldEffectuate = false;
            }
        }
        else
        {
            metric.Increment();

            _currentPoints += metric._cost;

            PointsGainedAnimation();
        }

        if (!couldEffectuate)
        {
            NotEnoughPointsAnimation();
        }
        UpdatePointsText();

        return couldEffectuate;
    }

    public bool OnDecrementMetricButtonPressed(Metric metric)
    {
        bool couldEffectuate = true;
        bool isSpendingPoints = metric._value <= metric._defaultValue;

        if (isSpendingPoints)
        {
            if (metric._cost <= _currentPoints)
            {
                metric.Decrement();

                _currentPoints -= metric._cost;

                PointsSpentAnimation();
            }
            else
            {
                couldEffectuate = false;
            }
        }
        else
        {
            metric.Decrement();

            _currentPoints += metric._cost;

            PointsGainedAnimation();
        }
        
        if (!couldEffectuate)
        {
            NotEnoughPointsAnimation();
        }
        UpdatePointsText();

        return couldEffectuate;
    }



    private void UpdatePointsText()
    {
        _pointsText.text = _currentPoints.ToString();

        if (_currentPoints == _maxPoints)
        {
            _resetPointsButton.Hide();
        }
        else
        {
            _resetPointsButton.Show();
        }
    }

    private void PointsGainedAnimation()
    {
        _pointsText.DOComplete();
        _pointsText.transform.DOComplete();

        float duration = 0.5f;

        _pointsText.transform.DOPunchScale(Vector3.one * 0.2f, duration, 3);
        _pointsText.DOColor(_pointsGainedColor, duration).OnComplete(() => _pointsText.DOColor(_defaultPointsColor, duration));
    }

    private void PointsSpentAnimation()
    {
        _pointsText.transform.DOComplete();

        float duration = 0.5f;

        Vector3 punch = Vector3.right * 1.0f + Vector3.down * 0.3f;
        _pointsText.transform.DOPunchScale(punch, duration, 7);
    }

    private void NotEnoughPointsAnimation()
    {
        _pointsText.DOComplete();
        _pointsText.transform.DOComplete();

        float duration = 0.5f;
        
        _pointsText.transform.DOPunchPosition(Vector3.left * 10.0f, duration);
        _pointsText.DOColor(_notEnoughPointsColor, duration).OnComplete(()=> _pointsText.DOColor(_defaultPointsColor, duration));
    }



    private void OnResetPointsButtonPressed()
    {
        OnPointsReset?.Invoke();

        _currentPoints = _maxPoints;
        UpdatePointsText();

        _resetPointsButton.ClickedPunch();

        _resetPointsButton.transform.rotation = Quaternion.identity;
        _resetPointsButton.transform.DORotate(transform.rotation.eulerAngles + (Vector3.forward * -180.0f), 0.4f);
    }


    private void PointsTextFloatUp()
    {
        _pointsText.rectTransform.DOScale(Vector3.one * 1.2f, 1.5f);
        _pointsText.rectTransform.DOBlendableMoveBy(Vector3.up * 7f, 1.5f)
            .OnComplete(() => { PointsTextFloatDown(); });
    }

    private void PointsTextFloatDown()
    {
        _pointsText.rectTransform.DOScale(Vector3.one, 1.5f);
        _pointsText.rectTransform.DOBlendableMoveBy(Vector3.down * 7f, 1.5f)
            .OnComplete(() => { PointsTextFloatUp(); });
    }

    private void PointstextStopFloat()
    {
        _pointsText.rectTransform.DOKill(false);
        _pointsText.rectTransform.localPosition = _pointsTextLocalPosition;
    }


    private void OnCloseButtonClicked()
    {
        ShowOptionsCanvas();

        _closeButton.ClickedPunch();

        _isInSubMenu = true;
    }
    private void OnBackToGameButtonClicked()
    {
        HideOptionsCanvas();

        _isInSubMenu = false;
    }
    private void OnConfirmCloseButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");

        _isInSubMenu = false;
    }


    private void ShowOptionsCanvas()
    {
        _optionsCanvas.SetActive(true);
    }
    private void HideOptionsCanvas()
    {
        _optionsCanvas.SetActive(false);
    }


}
