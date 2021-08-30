using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private Vector3 _lastPostion;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _lastPostion, 0.1f);
        _lastPostion = _target.position;
    }
}
