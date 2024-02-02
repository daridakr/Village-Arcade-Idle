using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Vampire
{
    public sealed class PlayerHealth : IDamageable
    {
        private HealthPoints _points;
        private UpgradeableArmor _armor;
        private bool _isAlive = true;

        public float ValueNormalazed => _points.Current / _points.Max;

        public event UnityAction<float> Changed;
        public UnityEvent<float> OnDealDamage { get; } = new UnityEvent<float>();
        public UnityEvent OnDeath { get; } = new UnityEvent();

        [Inject]
        private void Construct(HealthConfig config)
        {
            _points = new HealthPoints(config.Min, config.Max);
            _points.Changed += OnHealthChanged;

            // separate to other class
            _armor = new UpgradeableArmor();
            //_armor.Value = config.Armor;
        }

        public void Init(EntityManager entityManager, AbilityManager abilityManager, StatsManager statsManager)
        {
            OnDealDamage.AddListener(statsManager.IncreaseDamageDealt);

        }

        private void OnHealthChanged() => Changed?.Invoke(ValueNormalazed);

        public void GainHealth(float health)
        {
            _points.AddPoints(health);
        }

        public override void TakeDamage(float damage, Vector3 knockback = default)
        {
            if (_isAlive)
            {
                damage = ApplyArmor(damage);

                _points.SubtractPoints(damage);

                // Knockback
                //rb.velocity += knockback * Mathf.Sqrt(rb.drag);
                //statsManager.IncreaseDamageTaken(damage);
                //if (currentHealth <= 0)
                //{
                //    StartCoroutine(DeathAnimation());
                //}
                //else
                //{
                //    if (hitAnimationCoroutine != null) StopCoroutine(hitAnimationCoroutine);
                //    hitAnimationCoroutine = StartCoroutine(HitAnimation());
                //}
            }
        }

        private float ApplyArmor(float damage)
        {
            if (_armor.Value >= damage)
                return damage < 1 ? damage : 1;
            else
                return damage - _armor.Value;
        }

        public override void Knockback(Vector3 knockback)
        {
            //rb.velocity += knockback * Mathf.Sqrt(rb.drag);
        }

        private void OnDisable()
        {
            _points.Changed -= OnHealthChanged;
        }
    }
}