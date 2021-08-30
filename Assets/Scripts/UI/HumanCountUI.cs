using UnityEngine;
using TMPro;
using Zenject;

public class HumanCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _humanCount;

    private Snake _snake;

    [Inject]
    private void Construct(Snake snake)
    {
        _snake = snake;
    }

    private void Start()
    {
        _snake.HumanCountChanged += OnHumanCountChanged;
        SetGetCount(_snake.humanCount);
    }

    private void OnDisable()
    {
        _snake.GemCountChanged -= OnHumanCountChanged;
    }

    private void OnHumanCountChanged(int count)
    {
        SetGetCount(count);
    }

    private void SetGetCount(int count)
    {
        _humanCount.text = _humanCount.text = count.ToString();
    }
}
