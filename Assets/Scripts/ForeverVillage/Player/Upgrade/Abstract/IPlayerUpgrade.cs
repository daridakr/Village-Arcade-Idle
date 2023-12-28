using System;

namespace ForeverVillage.Scripts
{
    public interface IPlayerUpgrade
    {
        public int Level { get; }
        public int MaxLevel { get; }
        public int NextPrice { get; }

        public event Action<int> Upgraded;
    }
}