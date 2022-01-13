using UnityEngine;

namespace Pooling
{
    public class ObjectPoolMonoBehaviour : MonoBehaviour
    {
        private ObjectPool _objectPool;

        private void OnDisable()
        {
            if (!_objectPool) return;
            _objectPool.ReturnToPool(gameObject);
        }

        public void SetObjectPool(ObjectPool objectPool) => _objectPool = objectPool;
    }
}