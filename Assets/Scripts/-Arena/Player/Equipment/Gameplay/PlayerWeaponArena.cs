using System.Linq;

namespace Arena
{
    public class PlayerWeaponArena : PlayerWeapon,
        IWeaponDamager
    {
        public float TotalDamage => _weapons.Keys.Sum(x => x.Damage);
    }
}