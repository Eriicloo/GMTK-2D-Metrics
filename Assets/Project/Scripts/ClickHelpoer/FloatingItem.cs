using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private Transform _floatingTransform;    
    [SerializeField] private Vector3 _displacement = Vector3.up;
    [SerializeField] private float _duration = 1.0f;

    private Vector3 _startPosition;


    private void Awake()
    {
        _startPosition = _floatingTransform.position;
    }

    public void StartFloating()
    {
        FloatUp();
    }
    public void StopFloating()
    {
        _floatingTransform.DOComplete();
        _floatingTransform.DOMove(_startPosition, 0.001f);
    }
    
    public void Punch(Vector3 punch, float duration, Action onComplete)
    {
        _floatingTransform.DOComplete();
        _floatingTransform.DOPunchScale(punch, duration, 5).OnComplete(() => onComplete());
    }


    private void FloatUp()
    {
        _floatingTransform.DOBlendableMoveBy(_displacement, _duration)
            .OnComplete(() => { FloatDown(); });
    }

    private void FloatDown()
    {
        _floatingTransform.DOBlendableMoveBy(-_displacement, _duration)
            .OnComplete(() => { FloatUp(); });
    }
}
