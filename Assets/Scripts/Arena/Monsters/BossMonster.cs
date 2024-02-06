using Arena;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Vampire
{
    public class BossMonster : Monster
    {
        protected new BossMonsterBlueprint monsterBlueprint;
        protected BossAbility[] abilities;
        protected Coroutine act = null;
        public Rigidbody Rigidbody { get => _body; }
        public Animator Animator { get => _animator; }
        protected float timeSinceLastMeleeAttack;

        public override void Setup(int monsterIndex, Vector3 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            base.Setup(monsterIndex, position, monsterBlueprint, hpBuff);
            this.monsterBlueprint = (BossMonsterBlueprint) monsterBlueprint;
            abilities = new BossAbility[this.monsterBlueprint.abilityPrefabs.Length];
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i] = Instantiate(this.monsterBlueprint.abilityPrefabs[i], transform).GetComponent<BossAbility>();
                abilities[i].Init(this, _entityManager);
            }
            act = StartCoroutine(Act());
        }

        protected override void Update()
        {
            base.Update();
            timeSinceLastMeleeAttack += Time.deltaTime;
        }
        // protected override void FixedUpdate()
        // {
        //     base.FixedUpdate();
        //     // Vector2 moveDirection = (playerCharacter.transform.position - transform.position).normalized;
        //     // rb.velocity += moveDirection * monsterBlueprint.acceleration * Time.fixedDeltaTime;
        // }

        public void Move(Vector3 direction, float deltaTime)
        {
            _body.velocity += direction * monsterBlueprint.acceleration * deltaTime;
        }

        public void Freeze()
        {
            _body.velocity = Vector2.zero;
        }

        private IEnumerator Act()
        {
            while (true)
            {
                float[] abilityScores = abilities.Select(a => a.Score()).ToArray();
                float totalScore = abilityScores.Sum();
                float rand = Random.Range(0f, totalScore);
                float cumulative = 0;
                int abilityIndex = -1;
                for (int i = 0; i < abilities.Length; i++)
                {
                    abilities[i].Deactivate();
                    cumulative += abilityScores[i];
                    if (abilityIndex == -1 && rand < cumulative)
                        abilityIndex = i;
                }
                if (abilityIndex == -1)
                {
                    Debug.Log(totalScore);
                    yield return new WaitForSeconds(1);
                }
                else
                    yield return abilities[abilityIndex].Activate();
            }
        }

        protected override void DropLoot()
        {
            base.DropLoot();
            if (monsterBlueprint.chestBlueprint != null)
                _entityManager.SpawnChest(monsterBlueprint.chestBlueprint, transform.position);
        }

        public override IEnumerator Killed(bool killedByPlayer = true)
        {
            foreach (BossAbility ability in abilities)
                Destroy(ability.gameObject);
            StopCoroutine(act);
            yield return base.Killed(killedByPlayer);
        }

        protected override void OnPlayerHealthTriggerStay(PlayerHealth playerHealth)
        {
            if (((monsterBlueprint.meleeLayer & (1 << playerHealth.gameObject.layer)) != 0))
            {
                IDamageable damageable = playerHealth.GetComponentInParent<IDamageable>();
                Vector2 knockbackDirection = (damageable.transform.position - transform.position).normalized;
                if (timeSinceLastMeleeAttack > monsterBlueprint.meleeAttackDelay)
                {
                    damageable.TakeDamage(monsterBlueprint.meleeDamage, monsterBlueprint.meleeKnockback * knockbackDirection);
                    timeSinceLastMeleeAttack = 0;
                }
                else
                {
                    damageable.TakeDamage(0, monsterBlueprint.meleeKnockback * knockbackDirection);
                }
            }

            if (playerHealth.gameObject.TryGetComponent(out Chest chest))
            {
                chest.OpenChest(false);
            }
        }
    }
}
