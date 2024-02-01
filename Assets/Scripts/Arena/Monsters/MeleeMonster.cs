using DG.Tweening.Core.Easing;
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
            transform.position += moveDirection * monsterBlueprint.movespeed * Time.deltaTime;

            //Vector3 moveDirection = (_playerModel.transform.position - transform.position).normalized;
            //_body.velocity += moveDirection * monsterBlueprint.acceleration * Time.fixedDeltaTime;
            //_entityManager.Grid.UpdateClient(this);

            //Vector3 f = Vector3.zero;
            //int count = 0;
            //foreach (ISpatialHashGridClient client in _entityManager.Grid.FindNearbyInRadius(Position, 2))
            //{
            //    if (client != (ISpatialHashGridClient)this)
            //    {
            //        Vector3 diff = Position - client.Position;
            //        float dist = diff.magnitude;
            //        diff /= dist;
            //        f += diff / dist;
            //        count++;
            //    }
            //}
            //if (count > 0)
            //    f /= count;
            //_body.velocity += f * 0.1f * monsterBlueprint.acceleration * Time.fixedDeltaTime;
            //if (!knockedBack && _body.velocity.magnitude > monsterBlueprint.movespeed)
            //    _body.velocity = _body.velocity.normalized * monsterBlueprint.movespeed;

            //Debug.Log("move");
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
