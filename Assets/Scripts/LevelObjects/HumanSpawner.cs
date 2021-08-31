using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    [SerializeField] private Human _humanPrefub;
    [SerializeField] private float _groupSize;
    [SerializeField] private float _spawnArea;

    [SerializeField] private Color _color;

    private void Awake()
    {
        Activate(_color);
    }

    public void Activate(Color color)
    {
        for (int i = 0; i < _groupSize; i++)
        {
            Instantiate(_humanPrefub, transform.position + MathfHelper.RandomXZ(_spawnArea), Quaternion.identity).SetColor(color);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawCube(transform.position, new Vector3(_spawnArea, 0.5f, _spawnArea) * 2);
    }
}
