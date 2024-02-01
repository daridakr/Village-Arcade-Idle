using UnityEngine;

namespace Vampire
{
    public class RangedMonster : Monster
    {
        public enum State
        {
            Walking,
            Shooting
        }

        [SerializeField] protected Transform projectileSpawnPosition;
        protected new RangedMonsterBlueprint monsterBlueprint;
        protected float timeSinceLastAttack;
        protected State state;
        protected float outOfRangeTime;
        protected int projectileIndex;

        public override void Setup(int monsterIndex, Vector3 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            base.Setup(monsterIndex, position, monsterBlueprint, hpBuff);
            this.monsterBlueprint = (RangedMonsterBlueprint) monsterBlueprint;
            projectileIndex = _entityManager.AddPoolForProjectile(this.monsterBlueprint.projectilePrefab);
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
                        _entityManager.Grid.UpdateClient(this);
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
                            LaunchProjectile(dirToPlayer);
                            timeSinceLastAttack = 0;//Mathf.Repeat(timeSinceLastAttack, 1.0f/monsterBlueprint.atkspeed);
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

        protected void LaunchProjectile(Vector2 direction)
        {
            Projectile projectile = _entityManager.SpawnProjectile(projectileIndex, projectileSpawnPosition.position, monsterBlueprint.atk, 0, monsterBlueprint.projectileSpeed, monsterBlueprint.targetLayer);
            projectile.Launch(direction);
        }

        protected override void OnPlayerHealthTriggerStay(PlayerHealth playerHealth)
        {

        }
    }
}
