using UnityEngine;
using Zenject;

namespace Vampire
{
    public class HealthPoint : Collectable
    {
        [SerializeField] protected float healAmount = 30;

        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void OnCollected()
        {
            _playerHealth.GainHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
