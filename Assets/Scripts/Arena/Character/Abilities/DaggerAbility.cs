using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class DaggerAbility : StabAbility
    {
        [Header("Dagger Stats")]
        [SerializeField] protected UpgradeableBleedDamage bleedDamage;
        [SerializeField] protected UpgradeableBleedRate bleedRate;
        [SerializeField] protected UpgradeableBleedDuration bleedDuration;

        protected override void DamageMonster(Monster monster, float damage, Vector3 knockback)
        {
            base.DamageMonster(monster, damage, knockback);
            Coroutine monsterBleed = StartCoroutine(BleedMonster(monster));
            monster.OnKilled.AddListener( delegate { StopMonsterBleed(monsterBleed); } );
        }

        protected IEnumerator BleedMonster(Monster monster)
        {
            float bleedDelay = 1/bleedRate.Value;
            int bleedCount = Mathf.RoundToInt(bleedDuration.Value * bleedRate.Value);
            for (int i = 0; i < bleedCount; i++)
            {
                yield return new WaitForSeconds(bleedDelay);
                monster.TakeDamage(bleedDamage.Value);
                //_playerHealth.OnDealDamage.Invoke(bleedDamage.Value);
                if (monster.HP <= 0)
                    break;
            }
        }

        protected void StopMonsterBleed(Coroutine monsterBleed)
        {
            StopCoroutine(monsterBleed);
        }
    }
}
