using System.Collections;
using UnityEngine;

namespace Village
{
    public abstract class Dropper<T> : MonoBehaviour where T : DroppableItem
    {
        [SerializeField] private T _droppableItemTemplate;
        [SerializeField] private Vector3 _spawnOffset = new Vector3(0.38f, 2.2f, 0.0f);
        [SerializeField] private float _prePushDelay = 0.1f;

        private const float _pushForce = 10f;
        private const float _pushHeightExtra = 5f;
        private const float _spawnBetweenDelay = 0.1f;

        private Vector3 _spawnPosition => transform.position + _spawnOffset + Random.insideUnitSphere * 0.5f;

        public void Give(int count)
        {
            StartCoroutine(CreateItem(count));
        }

        private IEnumerator CreateItem(int count)
        {
            if (_prePushDelay != 0)
                yield return new WaitForSeconds(_prePushDelay);

            for (int i = 0; i < count; i++)
            {
                T spawnedItem = Instantiate(_droppableItemTemplate, _spawnPosition, Random.rotation);
                spawnedItem.Push(GetRandomDirection() * _pushForce);

                if (_spawnBetweenDelay != 0)
                    yield return new WaitForSeconds(_spawnBetweenDelay);
            }

            //PayCompleted?.Invoke();
        }

        private Vector3 GetRandomDirection()
        {
            var direction = Random.insideUnitSphere;
            direction.y = Mathf.Abs(direction.y);
            direction.y += _pushHeightExtra;

            return direction.normalized;
        }
    }
}
