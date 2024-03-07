using UnityEngine;

namespace ForeverVillage
{
    public interface IEquipableItem :
        IItem
    {
        // ����������, ����
        // ��� ����� ����������� - ��� �������������
        public Item Prefab { get; }
        public IItemPerk[] Perks { get; }
        public Item Equip(Transform rig);
    }
}