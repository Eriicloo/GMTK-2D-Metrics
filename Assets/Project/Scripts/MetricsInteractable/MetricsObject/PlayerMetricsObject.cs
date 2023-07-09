using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMetricsObject : MetricsObject
{
    [Header("COMPONENTS")]
    [SerializeField] private TextMeshProUGUI _metricsTitleText;
    [SerializeField] private Button _closeButton;

    [Header("METRICS")]
    [SerializeField] private string _metricsTitle;
    [SerializeField] public Metric _movementSpeed;
    [SerializeField] public Metric _jump;


    bool _alreadyInit = false;


    public override void Init(MetricsInteractableManager metricsInteractableManager)
    {
        if (_alreadyInit) return;

        _movementSpeed.Init(metricsInteractableManager);
        _jump.Init(metricsInteractableManager);


        _metricsTitleText.text = _metricsTitle;
        _closeButton.onClick.AddListener(metricsInteractableManager.HideCurrentInteractableMetrics);

        HideMetrics(false);

        _alreadyInit = true;
    }




}
