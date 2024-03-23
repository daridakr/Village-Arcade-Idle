namespace Arena
{
    public interface IWeaponCuster
        : ICusterPosition
    {
        public void Cust(ITargetsInfo targetsInfo, IPlayerWeaponDamager damager);
    }
}