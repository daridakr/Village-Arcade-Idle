using System;
using UnityEngine;

namespace Arena
{
    public abstract class Spell :
        ISpellMeta,
        ISpellEntity,
        ICustableSpell
    {
        private readonly SpellConfig _config;

        public string Title => _config.Title;
        public string Description => _config.Description;
        public Sprite Icon => _config.Icon;
        public float CastingTime => _config.CastingTime;
        public float LifeTime => _config.LifeTime;
        public float Cooldown => _config.Cooldown;

        protected ITransformCuster _custer;
        private float _lastCustTime = 0f;

        public event Action Custed;

        public Spell(SpellConfig config) => _config = config;

        public void StartCusting(ITransformCuster custer, ITargetsInfo targetInfo, float additionalDamage = 0)
        {
            ThrowExceptionIfParamsNotValid(custer, targetInfo);

            _custer = custer;
            float currentTime = Time.time;

            if (currentTime - _lastCustTime >= Cooldown)
            {
                Custed?.Invoke();

                _lastCustTime = currentTime;
                Cust(targetInfo, additionalDamage);
            }
        }

        private void Cust(ITargetsInfo targets, float additionalDamage = 0)
        {
            // start casting by casting time and when time is over invoke performSpell with life time

            Perform(targets, additionalDamage);
        }

        private void ThrowExceptionIfParamsNotValid(ITransformCuster custer, ITargetsInfo targetInfo)
        {
            if (custer == null)
                throw new NullReferenceException(Title + "have no custer - " + nameof(custer));

            if (targetInfo == null || targetInfo.All.Length <= 0)
                throw new NullReferenceException(Title + "have no target info - " + nameof(targetInfo));
        }

        protected abstract void Perform(ITargetsInfo targetInfo, float additionalDamage);
    }
}