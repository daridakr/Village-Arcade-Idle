using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public interface ICustomization
    {
        public string Title { get; }
        public Sprite Icon { get; }
        public UnityEngine.Object[] Customs { get; }
        public event Action<int> Changed;
    }
}

