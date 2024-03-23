using UnityEngine;

namespace ForeverVillage
{
    public interface IEquipableItem :
        IItem
    {
        // ����������, ����
        // ��� ����� ����������� - ��� �������������
        public GameObject Prefab { get; }
        public IItemPerk[] Perks { get; }
        public GameObject Equip(Transform rig);
    }
}