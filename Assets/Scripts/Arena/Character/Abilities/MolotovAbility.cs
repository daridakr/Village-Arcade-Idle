using System.Collections.Generic;
using UnityEngine;

namespace Vampire
{
    public class MolotovAbility : ThrowableAbility
    {
        [Header("Molotov Stats")]
        [SerializeField] protected UpgradeableDuration duration;
        [SerializeField] protected UpgradeableAOE fireRadius;
        [SerializeField] protected UpgradeableDamageRate fireDamageRate;

        protected override void LaunchThrowable()
        {
            MolotovThrowable throwable = (MolotovThrowable)entityManager.SpawnThrowable(throwableIndex, _playerModel.CenterTransform.position, damage.Value, knockback.Value, 0, monsterLayer);
            throwable.SetupFire(duration.Value, fireRadius.Value, fireDamageRate.Value);
            // Throw randomly at nearby enemies
            List<ISpatialHashGridClient> nearbyEnemies = entityManager.Grid.FindNearbyInRadius(_playerHealth.transform.position, throwRadius);

            Vector2 throwPosition;
            if (nearbyEnemies.Count > 0)
                throwPosition = nearbyEnemies[Random.Range(0, nearbyEnemies.Count)].Position;
            else
                throwPosition = (Vector2)_playerHealth.transform.position + Random.insideUnitCircle * throwRadius;
            throwable.Throw(throwPosition);
            throwable.OnHitDamageable.AddListener(_playerHealth.OnDealDamage.Invoke);
        }
    }
}
