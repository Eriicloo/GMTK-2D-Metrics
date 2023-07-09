using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static Action OnPlayerKilled;
    
    [Header("REFERENCES")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SpriteRenderer _sr;
    
    [SerializeField] private Transform _leftGroundedRay;
    [SerializeField] private Transform _rightGroundedRay;
    
    [Header("PARAMETERS")]
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private InstructionType _firstInstruction;

    [Header("METRICS")]
    [SerializeField] private PlayerMetricsObject _playerMetrics;

    private float _jumpCoef = 3;
    private float _trampJumpCoef = 3;
    private int _speed = 3;
    private int _health = 3;
    private int _maxHealth = 3;

    private bool _playing;
    
    private float _fanForce;
    private bool _inFan;

    private bool _run = false;
    private int _direction = 1;

    private Vector3 dist1;
    private Vector3 dist2;
    
    //Initial values
    private Vector3 _initialPosition;
    private bool _initialFlipX;
    private float _initialGravity;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialFlipX = _sr.flipX;
        _initialGravity = _rb.gravityScale;

        dist1 = _leftGroundedRay.transform.position - transform.position;
        dist2 = _rightGroundedRay.transform.position - transform.position;
    }

    private void OnEnable()
    {
        _playerMetrics._movementSpeed.OnValueChanged += OnMovementSpeedChanged;
        _playerMetrics._jump.OnValueChanged += OnJumpChanged;

        PlayLevelManager.OnPlayStart += StartPlaying;
        PlayLevelManager.OnReset += OnLevelReset;
    }
    
    private void OnDisable()
    {
        _playerMetrics._movementSpeed.OnValueChanged -= OnMovementSpeedChanged;
        _playerMetrics._jump.OnValueChanged -= OnJumpChanged;
            
        PlayLevelManager.OnPlayStart -= StartPlaying;;
        PlayLevelManager.OnReset -= OnLevelReset;
    }

    private bool IsGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, dist1.normalized, dist1.magnitude, LayerMask.GetMask("Default"));
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, dist2.normalized, dist2.magnitude, LayerMask.GetMask("Default"));

        if (leftHit || rightHit)
        {
            return true;
        }

        return false;
    }

    public void SetJumpCoef(int jumpCoef)
    {
        _jumpCoef = JumpCoefMapping(jumpCoef);
    }

    private float JumpCoefMapping(int jumpCoef)
    {
        switch (jumpCoef)
        {
            case 0:
                return 0;
                break;
            case 1:
                return 2.3f;
                break;
            case 2:
                return 3.15f;
                break;
            case 3:
                return 3.7f;
                break;
        }
        return 0;
    }

    public void SetSpeedCoef(int speed)
    {
        _speed = speed;
    }

    public void SetFanForce(int fanForce)
    {
        _fanForce = (float)fanForce;
    }

    public void SetHealth(int health)
    {
        _health = health;
        _maxHealth = _health;
    }

    private void FixedUpdate()
    {
        if (_run)
        {
            float totalForce = (float)_speed * _speedMultiplier * _direction;
            if (_inFan)
            {
                totalForce += _fanForce * _speedMultiplier;
            }
            _rb.velocity = new Vector2(totalForce, _rb.velocity.y);
        }

        if (_inFan)
        {
            
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
            SelectAction(other.gameObject.GetComponent<Instruction>()._instructionType);
        }
        else if (other.CompareTag("Boundries"))
        {
            OnPlayerKilled?.Invoke();
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
                Jump(_jumpCoef);
                break;
            case InstructionType.TRAMPOLINE:
                //TrampolineJump();
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
    
    private void Jump(float jumpCoef)
    {
        //jump animation
        if (!IsGrounded())
        {
            StartCoroutine(FakeCoyoteJump(jumpCoef));
        } else
        {
            _rb.AddForce(new Vector2(0, jumpCoef * _jumpMultiplier), ForceMode2D.Impulse);
        }
    }

    private IEnumerator FakeCoyoteJump(float jumpCoef)
    {
        yield return new WaitForSeconds(0.2f);
        if (IsGrounded())
        {
            _rb.AddForce(new Vector2(0, jumpCoef * _jumpMultiplier), ForceMode2D.Impulse);
        }
    }

    public void TrampolineJump(int trampolineCoef)
    {
        _trampJumpCoef = JumpCoefMapping(trampolineCoef);
        Jump(_trampJumpCoef);
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

    void OnLevelReset()
    {
        _playing = false;
        
        StopRunning();

        transform.position = _initialPosition;
        _sr.flipX = _initialFlipX;
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = _initialGravity;
    }
}
