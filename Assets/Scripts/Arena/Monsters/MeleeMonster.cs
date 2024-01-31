using UnityEngine;

namespace Vampire
{
    public class MeleeMonster : Monster
    {
        protected new MeleeMonsterBlueprint monsterBlueprint;
        protected float timeSinceLastAttack;

        public override void Setup(int monsterIndex, Vector3 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            base.Setup(monsterIndex, position, monsterBlueprint, hpBuff);
            this.monsterBlueprint = (MeleeMonsterBlueprint) monsterBlueprint;
        }

        protected override void Update()
        {
            base.Update();
            // Attack cooldown
            timeSinceLastAttack += Time.deltaTime;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector3 moveDirection = (_playerModel.transform.position - transform.position).normalized;
            _body.velocity += moveDirection * monsterBlueprint.acceleration * Time.fixedDeltaTime;
            entityManager.Grid.UpdateClient(this);

            // Vector2 f = Vector2.zero;
            // int count = 0;
            // foreach (ISpatialHashGridClient client in entityManager.Grid.FindNearbyInRadius(Position, 2))
            // {
            //     if (client != (ISpatialHashGridClient)this)
            //     {
            //         Vector2 diff = Position - client.Position;
            //         float dist = diff.magnitude;
            //         diff /= dist;
            //         f += diff / dist;
            //         count++;
            //     }
            // }
            // if (count > 0)
            //     f /= count;
            // rb.velocity += f * 0.1f * monsterBlueprint.acceleration * Time.fixedDeltaTime;
            // if (!knockedBack && rb.velocity.magnitude > monsterBlueprint.movespeed)
            //      rb.velocity = rb.velocity.normalized * monsterBlueprint.movespeed;
        }

        protected override void OnPlayerHealthTriggerStay(PlayerHealth playerHealth)
        {
            if (alive && ((monsterBlueprint.meleeLayer & (1 << playerHealth.gameObject.layer)) != 0) && timeSinceLastAttack >= 1.0f / monsterBlueprint.atkspeed)
            {
                playerHealth.TakeDamage(monsterBlueprint.atk);
                timeSinceLastAttack = Mathf.Repeat(timeSinceLastAttack, 1.0f / monsterBlueprint.atkspeed);
            }
        }
    }
}
