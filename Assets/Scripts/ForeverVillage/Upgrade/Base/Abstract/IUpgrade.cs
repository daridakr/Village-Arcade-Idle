using System;
using UnityEngine;

namespace Village
{
    public interface IUpgrade
    {
        public string Id { get; }
        public int Level { get; }
        public int MaxLevel { get; }
        public int NextPrice { get; }
        public string Title { get; }
        public string CurrentStats { get; }
        public string NextImprovement { get; }
        public Sprite Icon { get; }
        public bool IsMaxLevel { get; }
        public event Action<int> OnLevelUp;
        public void SetupLevel(int level);
    }
}