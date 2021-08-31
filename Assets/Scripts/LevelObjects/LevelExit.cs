using UnityEngine;
using Zenject;

public class LevelExit : MonoBehaviour
{
    private LevelResultUI _levelResultUI;

    [Inject]
    private void Construct(LevelResultUI levelResultUI)
    {
        _levelResultUI = levelResultUI;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snake snake))
        {
            snake.Win();
            _levelResultUI.ShowWin();
        }
    }
}
