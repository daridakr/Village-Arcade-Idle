using UnityEngine;

namespace Vampire
{
    public class BoomerangMonster : Monster
    {
        [SerializeField] protected Transform boomerangSpawnPosition;
        protected new BoomerangMonsterBlueprint monsterBlueprint;
        protected float timeSinceLastBoomerangAttack;
        protected float timeSinceLastMeleeAttack;
        protected float outOfRangeTime;
        protected int boomerangIndex;

        public override void Setup(int monsterIndex, Vector2 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            base.Setup(monsterIndex, position, monsterBlueprint, hpBuff);
            this.monsterBlueprint = (BoomerangMonsterBlueprint) monsterBlueprint;
            boomerangIndex = entityManager.AddPoolForBoomerang(this.monsterBlueprint.boomerangPrefab);
            outOfRangeTime = 0;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector3 toPlayer = (_playerModel.transform.position - transform.position);
            float distance = toPlayer.magnitude;
            Vector3 dirToPlayer = toPlayer/distance;
            entityManager.Grid.UpdateClient(this);
            timeSinceLastBoomerangAttack += Time.fixedDeltaTime;
            if (distance <= monsterBlueprint.range)
            {
                _body.velocity += dirToPlayer * monsterBlueprint.acceleration * Time.fixedDeltaTime / 2;
                if (timeSinceLastBoomerangAttack >= 1.0f/monsterBlueprint.boomerangAttackSpeed)
                {
                    ThrowBoomerang(_playerModel.transform.position);
                    timeSinceLastBoomerangAttack = 0;
                }
            }
            else
            {
                _body.velocity += dirToPlayer * monsterBlueprint.acceleration * Time.fixedDeltaTime;
            }
        }

        protected void ThrowBoomerang(Vector2 targetPosition)
        {
            Boomerang boomerang = entityManager.SpawnBoomerang(boomerangIndex, boomerangSpawnPosition.position, monsterBlueprint.boomerangDamage, 0, monsterBlueprint.throwRange, monsterBlueprint.throwTime, monsterBlueprint.targetLayer);
            boomerang.Throw(boomerangSpawnPosition, targetPosition);
        }

        protected override void OnPlayerHealthTriggerStay(PlayerHealth playerHealth)
        {
            if (((monsterBlueprint.targetLayer & (1 << playerHealth.gameObject.layer)) != 0) && timeSinceLastMeleeAttack >= 1.0f / monsterBlueprint.atkspeed)
            {
                playerHealth.TakeDamage(monsterBlueprint.atk);
                timeSinceLastMeleeAttack = 0;
            }
        }
    }
}
