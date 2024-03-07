namespace ForeverVillage
{
    public interface IEquipableItem :
        IItem, IEquipable
    {
        public IItemPerk[] Perks { get; }
    }
}