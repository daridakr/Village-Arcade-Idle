using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public interface ISpecialization
    {
        public string Title { get; }
        public string Description { get; }
        public Sprite Icon { get; }
    }
}