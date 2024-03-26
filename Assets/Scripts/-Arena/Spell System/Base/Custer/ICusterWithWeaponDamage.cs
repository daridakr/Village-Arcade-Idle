namespace Arena
{
    public interface ICusterWithWeaponDamage
        : ITransformCuster
    {
        public void Cust(ITargetsInfo targetsInfo, IWeaponDamager weapon);
    }
}