using System;
using UnityEngine;
using Zenject;

namespace Arena
{
    public abstract class Spell : TickableManager,
        ISpellMeta,
        ISpellEntity,
        ISpellCuster,
        ITickable
    {
        public string Title => _config.Title;
        public string Description => _config.Description;
        public Sprite Icon => _config.Icon;
        public float CastingTime => _config.CastingTime;
        public float LifeTime => _config.LifeTime;
        public float Cooldown => _config.Cooldown;

        private readonly SpellConfig _config;
        private float _lastCustTime = 0f;

        public event Action Custed;

        public bool CanUseAbility() => Time.time >= _lastCustTime + Cooldown;

        public Spell(SpellConfig config)
        {
            _config = config;
        }

        public void StartCusting(ITargetsInfo targets)
        {
            if (targets == null || targets.All.Length <= 0)
                throw new NullReferenceException(nameof(targets));

            float currentTime = Time.time;

            if (currentTime - _lastCustTime >= Cooldown)
            {
                _lastCustTime = currentTime;
                Cust(targets);
            }
        }

        private void Cust(ITargetsInfo targets)
        {
            // start casting by casting time and when time is over invoke performSpell with life time
            // reset cooldown
            Perform(targets);
            Custed?.Invoke();
        }

        public void Tick()
        {
            //_timeSinceLastCast += Time.deltaTime;

            //if (_timeSinceLastCast >= Cooldown && !_canCast)
            //{
            //    _timeSinceLastCast = Mathf.Repeat(_timeSinceLastCast, Cooldown);
            //    _canCast = true;
            //}
        }

        //protected abstract void Perform();
        protected abstract void Perform(ITargetsInfo targetInfo); // templtate pattern
    }
}