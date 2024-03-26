using UnityEngine;

namespace Village
{
    public interface IStorableObject
    {
        string Name { get; }
        string Description { get; }
        int Price { get; }
        Sprite Icon { get; }
    }
}