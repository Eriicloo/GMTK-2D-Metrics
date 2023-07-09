using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FanMetricsObject : MetricsObject
{
    [Header("COMPONENTS")]
    [SerializeField] private TextMeshProUGUI _metricsTitleText;
    [SerializeField] private Button _closeButton;

    [Header("METRICS")]
    [SerializeField] private string _metricsTitle;
    [SerializeField] public Metric _fanForce;
    
    public override void Init(MetricsInteractableManager metricsInteractableManager)
    {
        _fanForce.Init(metricsInteractableManager);


        _metricsTitleText.text = _metricsTitle;
        _closeButton.onClick.AddListener(metricsInteractableManager.HideCurrentInteractableMetrics);

        HideMetrics();
    }
}
