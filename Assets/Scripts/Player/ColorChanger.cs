using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color;

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
        if (other.TryGetComponent(out Snake snake))
        {
            snake.SetColor(_color);
            Destroy(this);
        }
    }
}
