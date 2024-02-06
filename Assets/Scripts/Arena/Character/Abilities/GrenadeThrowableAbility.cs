using UnityEngine;

namespace Vampire
{
    public class GrenadeThrowableAbility : ThrowableAbility
    {
        [Header("Grenade Stats")]
        [SerializeField] protected UpgradeableProjectileCount fragmentCount;

        protected override void LaunchThrowable()
        {
            GrenadeThrowable throwable = (GrenadeThrowable) entityManager.SpawnThrowable(throwableIndex, _playerModel.CenterTransform.position, damage.Value, knockback.Value, 0, monsterLayer);
            throwable.SetupGrenade(fragmentCount.Value);
            throwable.Throw((Vector2)_playerHealth.transform.position + Random.insideUnitCircle * throwRadius);
            //throwable.OnHitDamageable.AddListener(_playerHealth.OnDealDamage.Invoke);
        }
    }
}
