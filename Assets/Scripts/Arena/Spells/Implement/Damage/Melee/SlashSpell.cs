using UnityEngine;

namespace Arena
{
    public class SlashSpell : DamageSpell
    {
        public float Range => _config.Range;
        public float Speed => _config.Speed;
        public float SlashTime => _config.SlashTime;

        private readonly SlashSpellConfig _config;

        public SlashSpell(SlashSpellConfig config) : base(config)
        {
            _config = config;
            Debug.Log("Instantiate slash spell");
        }

        protected override void Perform(ITargetsInfo targetsInfo)
        {
            base.Perform(targetsInfo);

        }
    }
}