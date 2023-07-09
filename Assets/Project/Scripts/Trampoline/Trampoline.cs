using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    // Start is called before the first frame update
    public TrampolineMetricsObject trampolineMetricsObject;
    public Animator animator;
    int springForce = 1;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        trampolineMetricsObject._springForce.OnValueChanged += OnSpringForceChanged;

        PlayLevelManager.OnPlayStart += StartPlaying;
        PlayLevelManager.OnReset += OnLevelReset;
    }

    private void OnDisable()
    {
        trampolineMetricsObject._springForce.OnValueChanged -= OnSpringForceChanged;

        PlayLevelManager.OnPlayStart -= StartPlaying; ;
        PlayLevelManager.OnReset -= OnLevelReset;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetTrigger("PlayTrampoline");
            collision.GetComponent<PlayerController>().TrampolineJump(springForce);

        }
    }

    private void OnSpringForceChanged(Metric metric)
    {
        springForce = metric._value;
    }

    private void StartPlaying()
    {

    }
    private void OnLevelReset()
    {

    }
}
