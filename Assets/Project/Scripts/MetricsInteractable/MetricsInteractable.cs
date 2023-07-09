using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricsInteractable : MonoBehaviour
{
    [SerializeField] private MetricsObject _metricsObject;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Material _spriteMaterial;

    private MetricsInteractableManager _manager;

    private bool _isDisplaying;


    public void Init(MetricsInteractableManager manager)
    {
        _manager = manager;
        _isDisplaying = false;

        _metricsObject.Init(manager);

        _spriteMaterial = _spriteRenderer.material;
        ShowOutline();
    }

    private void OnEnable()
    {
        PlayLevelManager.OnPlayStart += HideOutline;
        PlayLevelManager.OnReset += ShowOutline;
    }
    private void OnDisable()
    {
        PlayLevelManager.OnPlayStart -= HideOutline;
        PlayLevelManager.OnReset -= ShowOutline;
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

        SetOutlineColor(_manager._activeInteractableOutline);
    }

    public void StopDisplaying()
    {
        _isDisplaying = false;
        _metricsObject.HideMetrics();

        SetOutlineColor(_manager._defaultInteractableOutline);
    }


    public void ShowOutline()
    {
        _spriteMaterial.SetFloat("_DrawOutline", 1.0f);
    }
    public void HideOutline()
    {
        _spriteMaterial.SetFloat("_DrawOutline", 0.0f);
    }
    public void SetOutlineColor(Color color)
    {
        _spriteMaterial.SetColor("_OutlineColor", color);
    }


}
