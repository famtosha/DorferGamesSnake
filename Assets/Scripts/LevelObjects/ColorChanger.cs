using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color;
    private bool _isUsed = false;

    private void Awake()
    {
        SetColor(_color);
    }

    public void SetColor(Color color)
    {
        _color = color;
        GetComponent<MeshRenderer>().material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Segment tail))
        {
            tail.SetColor(_color);
        }
        if (other.TryGetComponent(out Snake snake) && !_isUsed)
        {
            snake.SetColor(_color);
            _isUsed = true;
        }
    }
}
