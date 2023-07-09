using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public FanMetricsObject _fanMetricsObject;
    public AreaEffector2D _areaEffector2D;
    int _fanForce = 1;
    
    private void OnEnable()
    {
        _fanMetricsObject._fanForce.OnValueChanged += OnSpringForceChanged;

        PlayLevelManager.OnPlayStart += StartPlaying;
        PlayLevelManager.OnReset += OnLevelReset;
    }

    private void OnDisable()
    {
        _fanMetricsObject._fanForce.OnValueChanged -= OnSpringForceChanged;

        PlayLevelManager.OnPlayStart -= StartPlaying; ;
        PlayLevelManager.OnReset -= OnLevelReset;
    }

    private void OnSpringForceChanged(Metric metric)
    {
        _fanForce = metric._value;
        
        if (_fanForce == 0){
            _fanForce = 0;
        } else if (_fanForce == 1) {
            _fanForce = 50;
        } else if (_fanForce == 2) {
            _fanForce = 58;
        } else if (_fanForce == 3) {
            _fanForce = 72;
        }

        _areaEffector2D.forceMagnitude = _fanForce;
    }

    private void StartPlaying()
    {

    }
    private void OnLevelReset()
    {

    }
}
