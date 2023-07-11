using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [HideInInspector] public bool _wasCollected;

    [SerializeField] private FloatingItem _floatingItem;
    public Animator animator;

    private void Awake()
    {
        _wasCollected = false;
        _floatingItem.StartFloating();
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
        animator.SetBool("IsPicked", true);
        AudioManager.Instance.PlaySounds("Collectible");
        _wasCollected = true;
    }

    private void DeactivateCollectible()
    {
        gameObject.SetActive(false);
    }

    public void ResetCollectible()
    {
        gameObject.SetActive(true);
        animator.SetBool("IsPicked", false);
        _wasCollected = false;
    }
}
