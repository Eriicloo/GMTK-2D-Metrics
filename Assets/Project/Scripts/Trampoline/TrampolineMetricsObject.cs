using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrampolineMetricsObject : MetricsObject
{
    [Header("COMPONENTS")]
    [SerializeField] private TextMeshProUGUI _metricsTitleText;
    [SerializeField] private Button _closeButton;

    [Header("METRICS")]
    [SerializeField] private string _metricsTitle;
    [SerializeField] public Metric _springForce;


    public override void Init(MetricsInteractableManager metricsInteractableManager)
    {
        _springForce.Init(metricsInteractableManager);


        _metricsTitleText.text = _metricsTitle;
        _closeButton.onClick.AddListener(metricsInteractableManager.HideCurrentInteractableMetrics);

        HideMetrics();
    }




}
