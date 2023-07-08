using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricsInteractable : MonoBehaviour
{
    [SerializeField] private MetricsObject _metricsObject;

    private MetricsInteractableManager _manager;

    private bool _isDisplaying;


    public void Init(MetricsInteractableManager manager)
    {
        _manager = manager;
        _isDisplaying = false;

        _metricsObject.Init(manager);
    }



    private void OnMouseDown()
    {
        if (_manager.CanDisplay())
        {
            if (!_isDisplaying)
            {
                _manager.DisplayMetricsInteractable(this);
            }
            else
            {
                _manager.HideCurrentInteractableMetrics();
            }
        }
        
    }



    public void StartDisplaying()
    {
        _isDisplaying = true;
        _metricsObject.ShowMetrics();
    }

    public void StopDisplaying()
    {
        _isDisplaying = false;
        _metricsObject.HideMetrics();
    }


}
