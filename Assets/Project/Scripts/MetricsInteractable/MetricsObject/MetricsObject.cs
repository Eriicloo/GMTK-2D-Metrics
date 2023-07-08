using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MetricsObject : MonoBehaviour
{
    [SerializeField] private GameObject _metricsHUD;

    public abstract void Init(MetricsInteractableManager metricsInteractableManager);
    public void ShowMetrics()
    {
        _metricsHUD.SetActive(true);
    }
    public void HideMetrics()
    {
        _metricsHUD.SetActive(false);
    }
}
