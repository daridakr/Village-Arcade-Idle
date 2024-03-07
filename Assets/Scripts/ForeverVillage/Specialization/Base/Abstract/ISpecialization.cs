using Arena;
using UnityEngine;

namespace Village
{
    public interface ISpecialization
    {
        public string Title { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public Spell[] Spells { get; }
        //public IEquippable Weapon { get; }
    }
}
