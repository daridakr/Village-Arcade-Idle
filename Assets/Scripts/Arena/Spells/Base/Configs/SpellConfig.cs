using System;
using UnityEngine;

namespace Arena
{
    public abstract class SpellConfig : ScriptableObject
    {
        [SerializeField][Range(0, 1)] protected float _castingTime = 1f; // how long it takes to cast
        [SerializeField][Range(-1, 1)] protected float _lifeTime; // how long the spell will keep
        [SerializeField][Min(0)] private float _cooldownTime; // spell recovery time

        public float CastingTime => _castingTime;
        public float LifeTime => _lifeTime;
        public float Cooldown => _cooldownTime;

        public bool IsPermanent => _lifeTime == -1;
        public bool IsOneTime => _lifeTime == 0;
        public bool IsImmediate => _castingTime == 0;

        private void OnValidate()
        {
            try
            {
                Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected virtual void Validate()
        {
            if (IsImmediate) _cooldownTime = 0.3f;
        }
    }
}