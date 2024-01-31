using UnityEngine;
using Zenject;

namespace Vampire
{
    public class Pool : MonoBehaviour
    {
        protected ArenaPlayerCharacterModel _playerModel;

        protected EntityManager entityManager;
        protected GameObject prefab;
        protected bool collectionCheck = true;
        protected int defaultCapacity = 10;
        protected int maxSize = 10000;

        [Inject]
        private void Construct(ArenaPlayerCharacterModel player)
        {
            _playerModel = player;
        }

        public virtual void Init(EntityManager entityManager, GameObject prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            this.entityManager = entityManager;
            this.prefab = prefab;
            this.collectionCheck = collectionCheck;
            this.defaultCapacity = defaultCapacity;
            this.maxSize = maxSize;
        }
    }
}
