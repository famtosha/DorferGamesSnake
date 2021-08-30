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

    private PlayerInput _playerInput;
    private SceneLoader _sceneLoader;

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
        var newPosition = new Vector3(_playerInput.cursorPosition * _roadSize, transform.position.y, transform.position.z);
        newPosition += new Vector3(0, 0, _movementSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    public void CollectGem()
    {
        gemCount++;
    }

    public void HumanTouch()
    {
        humanCount++;
    }

    public void EnterTrap()
    {
        _sceneLoader.ReloadActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<Crystal>())
        {
            CollectGem();
            Destroy(other.gameObject);
        }
        if (other.HasComponent<Human>())
        {
            HumanTouch();
            Destroy(other.gameObject);
        }
        if (other.HasComponent<Trap>())
        {
            EnterTrap();
        }
    }
}
