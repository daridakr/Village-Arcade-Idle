using System;
using UnityEngine;
using Zenject;

namespace Arena
{
    public abstract class Spell : TickableManager,
        ISpellMeta,
        ISpellEntity,
        ICustedSpell
    {
        public string Title => _config.Title;
        public string Description => _config.Description;
        public Sprite Icon => _config.Icon;
        public float CastingTime => _config.CastingTime;
        public float LifeTime => _config.LifeTime;
        public float Cooldown => _config.Cooldown;

        protected ICusterPosition _custer;

        private readonly SpellConfig _config;
        private float _lastCustTime = 0f;

        public event Action Custed;

        public bool CanUseAbility() => Time.time >= _lastCustTime + Cooldown;

        public Spell(SpellConfig config)
        {
            _config = config;
        }

        public void StartCusting(ICusterPosition custer, ITargetsInfo targets)
        {
            if (targets == null || targets.All.Length <= 0)
                throw new NullReferenceException(nameof(targets));

            _custer = custer;
            float currentTime = Time.time;

            if (currentTime - _lastCustTime >= Cooldown)
            {
                Custed?.Invoke();

                _lastCustTime = currentTime;
                Cust(targets);
            }
        }

        private void Cust(ITargetsInfo targets)
        {
            // start casting by casting time and when time is over invoke performSpell with life time
            // reset cooldown

            Perform(targets);
        }

        //protected abstract void Perform();
        protected abstract void Perform(ITargetsInfo targetInfo); // templtate pattern
    }
}