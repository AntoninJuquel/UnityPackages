﻿using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<Pool> pools = new List<Pool>();
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        public void StartPool()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (var pool in pools)
            {
                pool.key = pool.prefab.name;

                var objectPool = new Queue<GameObject>();

                for (var i = 0; i < pool.size; i++)
                {
                    var obj = Instantiate(pool.prefab);
                    obj.name = pool.key;
                    obj.SetActive(false);
                    var controller = obj.AddComponent<ObjectPoolMonoBehaviour>();
                    controller.SetObjectPool(this);
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.key, objectPool);
            }
        }

        public GameObject SpawnFromPool(string key, Vector3 position, Quaternion rotation)
        {
            GameObject objectToSpawn;
            if (_poolDictionary[key].Count != 0)
            {
                objectToSpawn = _poolDictionary[key].Dequeue();
                objectToSpawn.transform.SetPositionAndRotation(position, rotation);
                objectToSpawn.SetActive(true);
            }
            else
            {
                objectToSpawn = Instantiate(pools.Find(p => p.key == key).prefab, position, rotation);
                objectToSpawn.name = key;
                var controller = objectToSpawn.AddComponent<ObjectPoolMonoBehaviour>();
                controller.SetObjectPool(this);
            }

            return objectToSpawn;
        }

        public void ReturnToPool(GameObject go) => _poolDictionary[go.name].Enqueue(go);
    }

    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int size;
    }
}