using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MetricsObject : MonoBehaviour
{
    [SerializeField] private GameObject _metricsHUD;


    public Action OnShow;
    public Action OnHide;


    public abstract void Init(MetricsInteractableManager metricsInteractableManager);
    public void ShowMetrics()
    {
        _metricsHUD.SetActive(true);

        OnShow?.Invoke();
    }
    public void HideMetrics(bool invokeEvent = false)
    {
        _metricsHUD.SetActive(false);

        if (invokeEvent)
        {
            OnHide?.Invoke();
        }        
    }
}
