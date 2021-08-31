using UnityEngine;

public class LevelResultUI : MonoBehaviour
{
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;

    public void ShowWin()
    {
        _winWindow.SetActive(true);
    }

    public void ShowLose()
    {
        _loseWindow.SetActive(true);
    }
}
