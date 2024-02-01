using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class ThrowingMonster : Monster
    {
        public enum State
        {
            Walking,
            Shooting
        }

        [SerializeField] protected Transform throwableSpawnPosition;
        protected new ThrowingMonsterBlueprint monsterBlueprint;
        protected float timeSinceLastAttack;
        protected State state;
        protected float outOfRangeTime;
        protected int throwableIndex;

        public override void Setup(int monsterIndex, Vector3 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            base.Setup(monsterIndex, position, monsterBlueprint, hpBuff);
            this.monsterBlueprint = (ThrowingMonsterBlueprint) monsterBlueprint;
            throwableIndex = _entityManager.AddPoolForThrowable(this.monsterBlueprint.throwablePrefab);
            outOfRangeTime = 0;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (alive)
            {
                Vector3 toPlayer = (_playerModel.transform.position - transform.position);
                float distance = toPlayer.magnitude;
                Vector3 dirToPlayer = toPlayer/distance;
                switch (state)
                {
                    case State.Walking:
                        _body.velocity += dirToPlayer * monsterBlueprint.acceleration * Time.fixedDeltaTime;
                        if (distance <= monsterBlueprint.range)
                        {
                            state = State.Shooting;
                            _animator.enabled = false;
                            //rb.bodyType = RigidbodyType2D.Static;
                            //rb.mass = 9999;
                        }
                        break;

                    case State.Shooting:
                        timeSinceLastAttack += Time.fixedDeltaTime;
                        // rb.velocity *= 0.95f;
                        if (timeSinceLastAttack >= 1.0f/monsterBlueprint.atkspeed)
                        {
                            LaunchThrowable(_playerModel.transform.position);
                            timeSinceLastAttack = 0;
                        }
                        if (distance <= monsterBlueprint.range)
                            outOfRangeTime = 0;
                        else
                            outOfRangeTime += Time.deltaTime;
                        if (outOfRangeTime > monsterBlueprint.timeAllowedOutsideRange)
                        {
                            state = State.Walking;
                            _animator.enabled = true;
                            //rb.bodyType = RigidbodyType2D.Dynamic;
                            //rb.mass = 1;
                        }
                        break;
                }
                // if (!knockedBack && rb.velocity.magnitude > monsterBlueprint.movespeed)
                //      rb.velocity = rb.velocity.normalized * monsterBlueprint.movespeed;
            }
        }

        protected void LaunchThrowable(Vector3 targetPosition)
        {
            Throwable throwable = _entityManager.SpawnThrowable(throwableIndex, throwableSpawnPosition.position, monsterBlueprint.atk, 0, -909, monsterBlueprint.targetLayer);
            targetPosition += _playerMovement.Velocity * throwable.ThrowTime;
            throwable.Throw(targetPosition);
        }

        public override IEnumerator Killed(bool killedByPlayer = true)
        {
            LaunchThrowable(transform.position);
            yield return base.Killed(killedByPlayer);
        }

        protected override void OnPlayerHealthTriggerStay(PlayerHealth playerHealth)
        {

        }
    }
}
