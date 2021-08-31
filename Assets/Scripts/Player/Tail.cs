using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tail : MonoBehaviour
{
    [SerializeField] private List<Transform> _segments;
    [SerializeField] private Timer _updateRate;
    [SerializeField] private Transform _head;

    private List<Vector3> _positionQeueue;
    private List<Quaternion> _rotationQueue;

    private void Awake()
    {
        _positionQeueue = new List<Vector3>(Enumerable.Repeat(transform.position, 32));
        _rotationQueue = new List<Quaternion>(Enumerable.Repeat(_head.rotation, 32));
    }

    private void Update()
    {
        _updateRate.UpdateTimer(Time.deltaTime);

        if (_updateRate.isReady)
        {
            _positionQeueue.Add(transform.position);
            _positionQeueue.RemoveAt(0);

            _rotationQueue.Add(_head.rotation);
            _rotationQueue.RemoveAt(0);
            _updateRate.Reset();
        }

        var segmentSize = _positionQeueue.Count / _segments.Count;
        for (int i = 0; i < _segments.Count; i++)
        {
            _segments[i].position = _positionQeueue[i * segmentSize];
            _segments[i].rotation = _rotationQueue[i * segmentSize];
        }
    }
}
