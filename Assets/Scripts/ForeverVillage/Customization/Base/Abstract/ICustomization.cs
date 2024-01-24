using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public interface ICustomization
    {
        public string Id { get; }
        public string Title { get; }
        public Sprite Icon { get; }
        public int Index { get; }
        public UnityEngine.Object[] Customs { get; }
        public event Action<int> Changed;
        public void Setup(int index);
    }
}

