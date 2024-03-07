namespace ForeverVillage
{
    public interface IWeapon
        : IEquipableItem
    {
        public float Damage { get; }
    }
}