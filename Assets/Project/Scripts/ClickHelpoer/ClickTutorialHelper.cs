using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTutorialHelper : MonoBehaviour
{
    [SerializeField] private FloatingItem _floatingItem;
    [SerializeField] private MetricsInteractable _interactable;

    private bool _wasUsed = false;


    private void Awake()
    {
        Show();
        _wasUsed = false;
    }

    private void OnEnable()
    {
        _interactable.OnMetricsDisplayClicked += Hide;
    }

    private void OnDisable()
    {
        _interactable.OnMetricsDisplayClicked -= Hide;
    }



    private void Show()
    {
        if (_wasUsed) return;

        gameObject.SetActive(true);
        _floatingItem.StartFloating();
    }

    private void Hide()
    {
        if (_wasUsed) return;

        
        _floatingItem.Punch(Vector3.one * 0.2f, 1.0f, () => 
        {
            gameObject.SetActive(false);
            _floatingItem.StopFloating();
            _wasUsed = true;
        }
        );

    }    

}
