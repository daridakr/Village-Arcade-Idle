namespace ForeverVillage
{
    public interface IWeapon
        : IEquipableItem
    {
        public float Damage { get; }
        public WeaponType Type { get; }
        public WeaponBodyPart BodyPart { get; }
    }
}