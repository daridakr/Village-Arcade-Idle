using System;

namespace Arena
{
    public interface IDamagable
    {
        public abstract void TakeDamage(float damage);
        public event Action<float> Damaged;
    }
}