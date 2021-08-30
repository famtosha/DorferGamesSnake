using System.Collections;
using UnityEngine;

public class Human : MonoBehaviour
{
    private Color _color;
    public Color color => _color;

    public void SetColor(Color newColor)
    {
        _color = newColor;
        GetComponent<MeshRenderer>().material.color = newColor;
    }
}
