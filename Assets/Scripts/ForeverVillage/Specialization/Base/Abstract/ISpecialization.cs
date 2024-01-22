using UnityEngine;

namespace ForeverVillage.Scripts
{
    public interface ISpecialization
    {
        public string Title { get; }
        public string Description { get; }
        public Sprite Icon { get; }
    }
}