using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Metric : MonoBehaviour
{
    [Header("NAME")]
    [SerializeField] public string _name = "Metric Name";

    [Header("STATS")]
    [SerializeField, Min(0)] public int _minValue = 1;
    [SerializeField, Min(0)] public int _maxValue = 3;
    [SerializeField, Min(0)] public int _defaultValue = 1;
    [HideInInspector] public int _value;
    [SerializeField, Min(1)] public int _cost = 1;

    [Header("COMPONENTS")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Button _incrementButton;
    [SerializeField] private Button _decrementButton;
    [SerializeField] private TextMeshProUGUI _minText;
    [SerializeField] private TextMeshProUGUI _maxText;
    [SerializeField] private TextMeshProUGUI _spentPointsText;


    private MetricsInteractableManager _metricsInteractableManager;

    public Action<Metric> OnValueChanged;


    private void OnEnable()
    {
        MetricsInteractableManager.OnPointsReset += ResetValue;
    }
    private void OnDisable()
    {
        MetricsInteractableManager.OnPointsReset -= ResetValue;
    }


    public void Init(MetricsInteractableManager metricsInteractableManager)
    {
        _metricsInteractableManager = metricsInteractableManager;

        _nameText.text = _name;
        _incrementButton.onClick.AddListener(OnIncrementButtonPressed);
        _decrementButton.onClick.AddListener(OnDecrementButtonPressed);

        ResetValue();
    }

    private void ResetValue()
    {
        _value = _defaultValue;

        UpdateValueText();
        UpdateSpentPointsText();

        if (_value == _minValue) HideButton(_decrementButton, _minText);
        else ShowButton(_decrementButton, _minText);

        if (_value == _maxValue) HideButton(_incrementButton, _maxText);
        else ShowButton(_incrementButton, _maxText);

        OnValueChanged?.Invoke(this);
    }

    public bool HasEnoughPoints(int points)
    {
        return points > _cost;
    }


    public bool IsValueAboveDefault()
    {
        return _value > _defaultValue;
    }

    public bool IsValueBellowDefault()
    {
        return _value < _defaultValue;
    }


    public bool CanIncrement()
    {
        return _value < _maxValue;
    }

    public bool CanDecrement()
    {
        return _value > _minValue;
    }


    private void OnIncrementButtonPressed()
    {
        if (_metricsInteractableManager.OnIncrementMetricButtonPressed(this))
        {

        }
        else
        {
            CanNotUseButtonAnimation(_incrementButton);
        }
    }

    public void Increment()
    {
        if (!CanDecrement())
        {
            ShowButton(_decrementButton, _minText);
        }

        ++_value;
        UpdateValueText();
        UpdateSpentPointsText();

        if (!CanIncrement())
        {
            HideButton(_incrementButton, _maxText);
        }

        OnValueChanged?.Invoke(this);
    }

    private void OnDecrementButtonPressed()
    {
        if (_metricsInteractableManager.OnDecrementMetricButtonPressed(this))
        {

        }
        else
        {
            CanNotUseButtonAnimation(_decrementButton);
        }
    }

    public void Decrement()
    {
        if (!CanIncrement())
        {
            ShowButton(_incrementButton, _maxText);
        }
        
        --_value;
        UpdateValueText();
        UpdateSpentPointsText();

        if (!CanDecrement())
        {
            HideButton(_decrementButton, _minText);
        }

        OnValueChanged?.Invoke(this);
    }


    private void ShowButton(Button button, TextMeshProUGUI limitText)
    {
        button.enabled = true;
        button.image.color = Color.white;
        //button.gameObject.SetActive(true);

        limitText.gameObject.SetActive(false);
    }
    private void HideButton(Button button, TextMeshProUGUI limitText)
    {
        button.enabled = false;
        button.image.color = Color.gray;
        //button.gameObject.SetActive(false);

        limitText.gameObject.SetActive(true);
    }


    private void UpdateValueText()
    {
        _valueText.text = _value.ToString();
    }


    private void CanNotUseButtonAnimation(Button button)
    {
        float duration = 0.5f;

        button.transform.DOComplete();
        button.image.DOComplete();

        button.transform.DOPunchPosition(Vector3.right * 10.0f, duration);
        button.image.DOColor(Color.red, duration / 2).OnComplete(() => button.image.DOColor(Color.white, duration / 2));
    }


    private void UpdateSpentPointsText()
    {
        int difference = _value - _defaultValue;

        if (difference == 0)
        {
            _valueText.color = Color.white;
        }
        else if (difference > 0)
        {
            _valueText.color = Color.green;
        }
        else if (difference < 0)
        {
            _valueText.color = Color.red;
        }
        
        
        if (difference == 0)
        {
            _spentPointsText.gameObject.SetActive(false);
        }
        else
        {
            _spentPointsText.gameObject.SetActive(true);

            string prefix = difference > 0 ? "+" : "-";
            difference = Mathf.Abs(difference);
            _spentPointsText.text = "(" + prefix + difference.ToString() + ")";
        }
    }

}
