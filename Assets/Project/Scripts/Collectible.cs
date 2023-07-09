using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [HideInInspector] public bool _wasCollected;

    private void Awake()
    {
        _wasCollected = true;
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D()
    {
        gameObject.SetActive(false);
    }
}
