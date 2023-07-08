using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MetricsInteractableManager : MonoBehaviour
{
    [Header("INTERACTABLES")]
    [SerializeField] private MetricsInteractable[] interactables;

    private MetricsInteractable _currentActiveInteractable;

    [Header("POINTS")]
    [SerializeField] private int _maxPoints = 3;
    [SerializeField] private int _currentPoints;
    [SerializeField] private TextMeshProUGUI _pointsText;


    private void Awake()
    {
        foreach (var interactable in interactables)
        {
            interactable.Init(this);
        }

        _currentActiveInteractable = null;

        _currentPoints = _maxPoints;
        UpdatePointsText();
    }


    public bool CanDisplay()
    {
        return true;
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
    }

    private void PointsGainedAnimation()
    {
        _pointsText.DOComplete();
        _pointsText.transform.DOComplete();

        float duration = 0.5f;

        _pointsText.transform.DOPunchScale(Vector3.one * 0.2f, duration, 3);
        _pointsText.DOColor(Color.green, duration).OnComplete(() => _pointsText.DOColor(Color.white, duration));
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
        _pointsText.DOColor(Color.red, duration).OnComplete(()=> _pointsText.DOColor(Color.white, duration));
    }


}
