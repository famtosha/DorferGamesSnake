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

    private PlayerInput _playerInput;
    private SceneLoader _sceneLoader;

    private bool _isBoosted => !_boostDuration.isReady;

    private Color _currentColor;

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
        print(_isBoosted);
        _boostDuration.UpdateTimer(Time.deltaTime);

        var currentPositionX = _isBoosted ? 0 : _playerInput.cursorPosition;
        var newPosition = new Vector3(currentPositionX * _roadSize, transform.position.y, transform.position.z);

        var currentSpeed = _isBoosted ? _boostedSpeed : _movementSpeed;
        newPosition += new Vector3(0, 0, currentSpeed * Time.deltaTime);


        transform.position = newPosition;
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
        GetComponent<MeshRenderer>().material.color = color;
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

    public void HumanTouch(Human human)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<Crystal>())
        {
            CollectGem();
            Destroy(other.gameObject);
        }
        if (other.TryGetComponent(out Human human))
        {
            HumanTouch(human);
            Destroy(other.gameObject);
        }
        if (other.TryGetComponent(out Trap trap))
        {
            EnterTrap(trap);
        }
    }

    public void Lose()
    {
        _sceneLoader.ReloadActiveScene();
    }
}
