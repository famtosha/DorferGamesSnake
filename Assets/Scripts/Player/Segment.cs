using UnityEngine;

public class Segment : MonoBehaviour
{
    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
