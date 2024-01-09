using UnityEngine;

namespace ForeverVillage.Scripts
{
    public interface IStorableObject
    {
        string Name { get; }
        string Description { get; }
        int Price { get; }
        Sprite Icon { get; }
    }
}