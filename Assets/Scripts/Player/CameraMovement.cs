using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _snake;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _snake.transform.position.z);
    }
}
