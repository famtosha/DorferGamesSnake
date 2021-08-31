using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] [Range(0, 1)] private float _speed;
    [SerializeField] [Min(0)] private float _minDistance;

    private Vector3 _lastPostion;
    private Quaternion _lastRoatation;

    private void Start()
    {
        _lastPostion = _target.position;
        _lastRoatation = _target.rotation;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) > _minDistance)
        {
            transform.position = Vector3.Lerp(transform.position, _lastPostion, _speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, _lastRoatation, _speed);

            _lastPostion = _target.position;
            _lastRoatation = _target.rotation;
        }
    }

    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
