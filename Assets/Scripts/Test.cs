using System.Collections.Generic;
using Pooling;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Bounds spawnBounds;
    private ObjectPool _objectPool;
    private List<GameObject> _objects = new List<GameObject>();

    private void Awake()
    {
        _objectPool = GetComponent<ObjectPool>();
        _objectPool.StartPool();
    }

    private void Update()
    {
        var min = spawnBounds.min;
        var max = spawnBounds.max;
        var position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        if (Input.GetMouseButtonDown(0))
            _objects.Add(_objectPool.SpawnFromPool("prefab", position, Quaternion.identity));
        if (Input.GetMouseButtonDown(1))
        {
            for (var i = _objects.Count - 1; i >= 0; i--)
            {
                _objects[i].SetActive(false);
                _objects.RemoveAt(i);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, .25f);
        Gizmos.DrawCube(spawnBounds.center, spawnBounds.extents);
    }
}