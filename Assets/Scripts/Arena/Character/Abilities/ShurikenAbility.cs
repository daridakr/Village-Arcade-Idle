using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class ShurikenAbility : ProjectileAbility
    {
        [Header("Shuriken Stats")]
        [SerializeField] protected UpgradeableProjectileCount projectileCount;
        [SerializeField] protected float shurikenDelay;

        protected override void Attack()
        {
            StartCoroutine(LuanchShurikens());
        }

        protected IEnumerator LuanchShurikens()
        {
            timeSinceLastAttack -= projectileCount.Value*shurikenDelay;

            for (int i = 0; i < projectileCount.Value; i++)
            {
                LaunchProjectile(_playerModel.LookDirection);
                yield return new WaitForSeconds(shurikenDelay);
            }
        }

        protected void LaunchProjectile(Vector2 direction)
        {
            Projectile projectile = entityManager.SpawnProjectile(projectileIndex, _playerModel.CenterTransform.position, damage.Value, knockback.Value, speed.Value, monsterLayer);
            projectile.OnHitDamageable.AddListener(_playerHealth.OnDealDamage.Invoke);
            projectile.Launch(direction);
        }
    }
}
