using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    
    [Header("PARAMETERS")]
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private InstructionType _firstInstruction;

    [Header("METRICS")]
    [SerializeField] private PlayerMetricsObject _playerMetrics;


    private float _jumpCoef = 3;
    private int _speed = 3;
    private int _health = 3;
    private int _maxHealth = 3;

    private bool _playing;

    private bool _run = false;
    private int _direction = 1;


    private void OnEnable()
    {
        _playerMetrics._movementSpeed.OnValueChanged += OnMovementSpeedChanged;
        _playerMetrics._jump.OnValueChanged += OnJumpChanged;
    }
    
    private void OnDisable()
    {
        _playerMetrics._movementSpeed.OnValueChanged -= OnMovementSpeedChanged;
        _playerMetrics._jump.OnValueChanged -= OnJumpChanged;
    }

    public void SetJumpCoef(int jumpCoef)
    {
        switch (jumpCoef)
        {
            case 0:
                _jumpCoef = 0;
                break;
            case 1:
                _jumpCoef = 2.3f;
                break;
            case 2:
                _jumpCoef = 3.15f;
                break;
            case 3:
                _jumpCoef = 3.7f;
                break;
        }
    }

    public void SetSpeedCoef(int speed)
    {
        _speed = speed;
    }

    public void SetHealth(int health)
    {
        _health = health;
        _maxHealth = _health;
    }
    
    private void Update()
    {
        //TEST
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPlaying();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        if (_run)
        {
            _rb.velocity = new Vector2((float)_speed * _speedMultiplier * _direction, _rb.velocity.y);
        }
    }

    public void StartPlaying()
    {
        _playing = true;
        
        SelectAction(_firstInstruction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Instruction"))
        {
            Debug.Log(other.gameObject.GetComponent<Instruction>()._instructionType);
            SelectAction(other.gameObject.GetComponent<Instruction>()._instructionType);
        }
    }

    private void SelectAction(InstructionType instructionType)
    {
        switch (instructionType)
        {
            case InstructionType.RUN_RIGHT:
                Run(1);
                break;
            case InstructionType.RUN_LEFT:
                Run(-1);
                break;
            case InstructionType.JUMP:
                Jump();
                break;
            case InstructionType.TRAMPOLINE:
                TrampolineJump();
                break;
        }
    }

    private void Run(int direction)
    {
        //run animation
        _run = true;
        _direction = direction;
    }

    private void StopRunning()
    {
        //idle animation
        _run = false;
    }
    
    private void Jump()
    {
        //jump animation
        _rb.AddForce(new Vector2(0, _jumpCoef * _jumpMultiplier), ForceMode2D.Impulse);
    }

    private void TrampolineJump()
    {
        //_rb.AddForce(new Vector2(0, GameStats.trampolineJumpCoef * _jumpMultiplier), ForceMode2D.Impulse);
    }

    private void Attack()
    {
        //attack animation
    }

    private void Heal(int healing)
    {
        //healing animation
        _health += healing;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    private void TakeDamage(int damageTaken)
    {
        //damage animation
        _health -= damageTaken;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }
    
    private void Die()
    {
        //die animation
    }



    private void OnMovementSpeedChanged(Metric metric)
    {        
        SetSpeedCoef(metric._value);
    }

    private void OnJumpChanged(Metric metric)
    {
        SetJumpCoef(metric._value);
    }

}
