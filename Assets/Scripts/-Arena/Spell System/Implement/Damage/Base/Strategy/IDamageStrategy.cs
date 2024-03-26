namespace Arena
{
    public interface IDamageStrategy
    {
        public void DealDamage(ITargetsInfo targetsInfo, float additionalDamage);
    }
}