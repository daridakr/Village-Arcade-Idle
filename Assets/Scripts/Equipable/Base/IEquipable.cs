namespace ForeverVillage
{
    public interface IEquipable
    {
        // требования, локи
        // кто может экипировать - тип специализации
        public void Equip();
        public void Unequip();
    }
}