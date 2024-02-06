using Arena;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Vampire
{
    public abstract class BossAbility : MonoBehaviour
    {
        [Header("Ability Details")]
        protected BossMonster monster;
        protected EntityManager entityManager;

        protected PlayerCharacterModelArena _playerModel;
        protected PlayerMovementArena _playerMovement;

        protected bool active = false;
        protected float useTime;

        [Inject]
        private void Construct(
            PlayerCharacterModelArena playerModel,
            PlayerMovementArena playerMovement)
        {
            _playerModel = playerModel;
            _playerMovement = playerMovement;
        }

        public virtual void Init(BossMonster monster, EntityManager entityManager)
        {
            this.monster = monster;
            this.entityManager = entityManager;
        }

        public abstract IEnumerator Activate();

        public virtual void Deactivate()
        {
            active = false;
            StopAllCoroutines();
        }

        // Gives the ability a score indicating how suitable it
        // would be to use the ability at the given moment
        public abstract float Score();
    }
}
