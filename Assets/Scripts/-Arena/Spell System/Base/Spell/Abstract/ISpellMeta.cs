using UnityEngine;

namespace Arena
{
    public interface ISpellMeta
    {
        public string Title { get; }
        public string Description { get; }
        public Sprite Icon { get; }
    }
}