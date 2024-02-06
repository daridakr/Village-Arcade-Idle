using System;
using UnityEngine;
using UnityEngine.Events;
using Vampire;
using Zenject;

public sealed class PlayerHealth : MonoBehaviour,
    IDamageSource, IDamageable
{
    private BarPoints _points;
    private HealthConfig _config;
    private UpgradeableArmor _armor;

    private bool _isAlive = true;

    public float ValueNormalazed => _points.Current / _points.Max;

    public event UnityAction<float> Changed;
    public UnityEvent<float> OnDealDamage { get; } = new UnityEvent<float>();
    public UnityEvent OnDeath { get; } = new UnityEvent();


    public StatsHandler StatsHandler { get; private set; }
    string ITargetable.TeamID { get; } = nameof(Player);
    Action<object> ITargetable.OnHit { get; }

    bool IDamageable.IsAlive { get; set; } = true;
    Action<IDamageable, float, bool> IDamageSource.OnDamageDealt { get; set; }
    Action<IDamageSource> IDamageable.OnDeath { get; set; }
    Action<IDamageSource, float, bool> IDamageable.OnDamageTaken { get; set; }
    Action<IDamageable> IDamageSource.OnKill { get; set; }

    [Inject]
    private void Construct(HealthConfig config)
    {
        _config = config;

        _points = new BarPoints(_config.Min, _config.Max);
        _points.Changed += OnHealthChanged;

        // separate to other class
        _armor = new UpgradeableArmor();
        //_armor.Value = config.Armor;
    }


    private void Awake()
    {
        StatsHandler = new StatsHandler();
        StatsHandler.InitializeHealthStats(_config.Max, 5f);
        //StatsHandler.InitializeManaStats(MaxMana, ManaRegen);
        //StatsHandler.InitializeSpecialStats(Strength, Dexterity, Intelligence);
        StatsHandler.InitializeSkillCastingRelatedStats();
        //StatsHandler.InitializeCriticalHitStats(CriticalChance);
        //StatsHandler.InitializeMiscStats(MovementSpeed, Armor, LifeSteal);
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

    //public override void TakeDamage(float damage, Vector3 knockback = default)
    //{
    //    if (_isAlive)
    //    {
    //        damage = ApplyArmor(damage);

    //        _points.SubtractPoints(damage);

    //        // Knockback
    //        //rb.velocity += knockback * Mathf.Sqrt(rb.drag);
    //        //statsManager.IncreaseDamageTaken(damage);
    //        //if (currentHealth <= 0)
    //        //{
    //        //    StartCoroutine(DeathAnimation());
    //        //}
    //        //else
    //        //{
    //        //    if (hitAnimationCoroutine != null) StopCoroutine(hitAnimationCoroutine);
    //        //    hitAnimationCoroutine = StartCoroutine(HitAnimation());
    //        //}
    //    }
    //}

    private float ApplyArmor(float damage)
    {
        if (_armor.Value >= damage)
            return damage < 1 ? damage : 1;
        else
            return damage - _armor.Value;
    }

    //public override void Knockback(Vector3 knockback)
    //{
    //    //rb.velocity += knockback * Mathf.Sqrt(rb.drag);
    //}

    private void OnDisable()
    {
        _points.Changed -= OnHealthChanged;
    }
}