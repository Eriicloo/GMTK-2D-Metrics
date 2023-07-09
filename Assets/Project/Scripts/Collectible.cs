using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [HideInInspector] public bool _wasCollected;

    private void Awake()
    {
        _wasCollected = false;
    }

    private void OnEnable()
    {
        PlayLevelManager.OnReset += ResetCollectible;
    }

    private void OnDisable()
    {
        PlayLevelManager.OnReset -= ResetCollectible;
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D()
    {
        AudioManager.Instance.PlaySounds("Collectible");
        gameObject.SetActive(false);
        _wasCollected = true;
    }

    void ResetCollectible()
    {
        gameObject.SetActive(true);
        _wasCollected = false;
    }
}
