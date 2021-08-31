using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
public class Snake : MonoBehaviour
{
    public event Action<int> GemCountChanged;
    public event Action<int> HumanCountChanged;

    [SerializeField] private float _roadSize;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _boostedSpeed;
    [SerializeField] private Timer _boostDuration;

    [SerializeField] private Transform _head;

    private PlayerInput _playerInput;
    private SceneLoader _sceneLoader;

    private bool _isBoosted => !_boostDuration.isReady;

    private Color _currentColor;

    private bool _movementEnabled = true;

    private int _gemCount;
    public int gemCount
    {
        get => _gemCount;
        set
        {
            _gemCount = value;
            GemCountChanged?.Invoke(value);
        }
    }

    private int _humanCount;
    public int humanCount
    {
        get => _humanCount;
        set
        {
            _humanCount = value;
            HumanCountChanged?.Invoke(value);
        }
    }

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        _boostDuration.UpdateTimer(Time.deltaTime);
        if (_movementEnabled)
        {
            RotateHead();
            Move();
        }
    }

    public void Win()
    {
        _movementEnabled = false;
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
    }

    public void CollectGem()
    {
        gemCount++;
        if (gemCount >= 3)
        {
            gemCount = 0;
            _boostDuration.Reset();
        }
    }

    public void CollectHuman(Human human)
    {
        if (human.color == _currentColor || _isBoosted)
        {
            humanCount++;
        }
        else
        {
            Lose();
        }
    }

    public void EnterTrap(Trap trap)
    {
        if (_isBoosted)
        {
            Destroy(trap.gameObject);
        }
        else
        {
            Lose();
        }
    }

    private void RotateHead()
    {
        if (_isBoosted)
        {
            _head.transform.forward = Vector3.forward;
        }
        else
        {
            var t = ((_playerInput.cursorPosition * _roadSize) - transform.position.x) + 0.5f;
            var lookDirection = Vector3.Lerp(Vector3.forward - Vector3.right, Vector3.forward + Vector3.right, t);
            _head.transform.forward = lookDirection;
        }
    }

    private void Move()
    {
        float newPositionX = _isBoosted ? 0 : _playerInput.cursorPosition * _roadSize;
        var currentSpeed = _isBoosted ? _boostedSpeed : _movementSpeed;
        var newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
        newPosition += new Vector3(0, 0, currentSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void Lose()
    {
        _movementEnabled = false;
        _sceneLoader.ReloadActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<Crystal>())
        {
            CollectGem();
            Destroy(other.gameObject);
        }
        if (other.TryGetComponent(out Human human))
        {
            CollectHuman(human);
            Destroy(other.gameObject);
        }
        if (other.TryGetComponent(out Trap trap))
        {
            EnterTrap(trap);
        }
    }
}
