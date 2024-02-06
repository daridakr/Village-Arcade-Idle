using Vampire;
using Zenject;

namespace Arena
{
    public sealed class PlayerHealth : Health
    {
        private PlayerHealthConfig _config;
        private UpgradeableArmor _armor;

        [Inject]
        private void Construct(PlayerHealthConfig config)
        {
            _config = config;
            InitPoints(_config);

            _armor = new UpgradeableArmor();
            _armor.Value = config.Armor;
        }

        public override void TakeDamage(float damage)
        {
            damage = ApplyArmor(damage);

            base.TakeDamage(damage);
        }

        private float ApplyArmor(float damage)
        {
            if (_armor.Value >= damage)
                return damage < 1 ? damage : 1;
            else
                return damage - _armor.Value;
        }
    }
}