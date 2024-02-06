using UnityEngine.Pool;
using UnityEngine;
using Arena;

namespace Vampire
{
    public class MonsterPool : Pool
    {
        protected ObjectPool<Monster> pool;
        protected PlayerMovementArena _playerMovement;

        public override void Init(EntityManager entityManager, GameObject prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            base.Init(entityManager, prefab, collectionCheck, defaultCapacity, maxSize);

            pool = new ObjectPool<Monster>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPooledItem, collectionCheck, defaultCapacity, maxSize);
        }

        public void InitPlayer(PlayerCharacterModelArena model, PlayerMovementArena movement)
        {
            _playerModel = model;
            _playerMovement = movement;
        }

        public Monster Get()
        {
            return pool.Get();
        }

        public void Release(Monster monster)
        {
            pool.Release(monster);
        }

        protected Monster CreatePooledItem()
        {
            Monster monster = Instantiate(prefab, transform).GetComponent<Monster>();
            monster.Init(entityManager, _playerModel, _playerMovement);
            return monster;
        }

        protected void OnTakeFromPool(Monster monster)
        {
            monster.gameObject.SetActive(true);
        }

        protected void OnReturnedToPool(Monster monster)
        {
            monster.gameObject.SetActive(false);
        }

        protected void OnDestroyPooledItem(Monster monster)
        {
            Destroy(monster.gameObject);
        }
    }
}
