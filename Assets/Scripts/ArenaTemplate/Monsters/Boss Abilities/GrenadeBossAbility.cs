using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class GrenadeBossAbility : BossAbility
    {
        [Header("Grenade Details")]
        [SerializeField] protected GameObject grenadePrefab;
        [SerializeField] protected LayerMask targetLayer;
        [SerializeField] protected float damage;
        [SerializeField] protected float knockback;
        [SerializeField] protected float fireRate;
        protected float timeInAir;
        protected float timeSinceLastAttack;
        protected int throwableIndex = -1;

        public override void Init(BossMonster monster, EntityManager entityManager)
        {
            base.Init(monster, entityManager);
            throwableIndex = entityManager.AddPoolForThrowable(grenadePrefab);
        }

        // void Update()
        // {
        //     if (active)
        //     {
        //         timeSinceLastAttack += Time.deltaTime;
        //         if (timeSinceLastAttack >= 1/fireRate)
        //         {
        //             timeSinceLastAttack = Mathf.Repeat(timeSinceLastAttack, 1/fireRate);
        //             LaunchGrenade();
        //         }
        //     }
        // }

        void FixedUpdate()
        {
            if (active)
            {
                Vector3 moveDirection = (_playerModel.transform.position - monster.transform.position).normalized;
                monster.Move(moveDirection, Time.fixedDeltaTime);
                entityManager.Grid.UpdateClient(monster);
            }
        }

        protected void LaunchGrenade()
        {
            Vector3 initialDir = (_playerModel.transform.position - monster.transform.position).normalized;
            Throwable throwable = entityManager.SpawnThrowable(throwableIndex, monster.CenterTransform.position, damage, knockback, timeInAir, targetLayer);
            Vector3 targetPosition = _playerModel.transform.position;
            //targetPosition += _playerMovement.Velocity * throwable.ThrowTime;
            throwable.Throw(targetPosition);
        }

        protected IEnumerator LaunchGrenadeRoutine()
        {
            LaunchGrenade();
            yield return new WaitForSeconds(1/fireRate);
        }

        public override IEnumerator Activate()
        {
            active = true;
            yield return StartCoroutine(LaunchGrenadeRoutine());
        }

        public override float Score()
        {
            float distance = Vector2.Distance(monster.transform.position, _playerModel.transform.position);
            float score = distance / (distance + 2);
            return score;
        }
    }
}
