using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class GemCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gemCount;

    private Snake _snake;

    [Inject]
    private void Construct(Snake snake)
    {
        _snake = snake;
    }

    private void Start()
    {
        _snake.GemCountChanged += OnGemCountChanged;
        SetGetCount(_snake.gemCount);
    }

    private void OnDisable()
    {
        _snake.GemCountChanged -= OnGemCountChanged;
    }

    private void OnGemCountChanged(int gemCount)
    {
        SetGetCount(gemCount);
    }

    private void SetGetCount(int gemCount)
    {
        _gemCount.text = _gemCount.text = gemCount.ToString();
    }
}
