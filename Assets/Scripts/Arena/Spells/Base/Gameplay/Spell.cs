using System;
using UnityEngine;

namespace Arena
{
    public abstract class Spell :
        ISpellEntity,
        ISpellAction
    {
        public float CastingTime => _config.CastingTime;
        public float LifeTime => _config.LifeTime;
        public float Cooldown => _config.Cooldown;

        private readonly SpellConfig _config;

        public float _cooldownStartTimeStamp; // Время начала перезарядки
        public float _cooldownEndTimeStamp;   // Время окончания перезарядки

        public event Action Custed;

        public Spell(SpellConfig config) => _config = config;

        public bool CanCast()
        {
            return _cooldownEndTimeStamp <= Time.time;
        }

        public void Cust(ITargetable target)
        {
            if (target == null)
                throw new NullReferenceException(nameof(target));

            if (CanCast())
            {
                // start casting by casting time and when time is over invoke performSpell with life time

                // reset cooldown
                Perform(LifeTime);
                Custed?.Invoke();
            }
        }

        protected abstract void Perform(float lifeTime); // templtate pattern
    }
}