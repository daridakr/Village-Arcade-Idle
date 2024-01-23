using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class Customization : ICustomization
    {
        [ReadOnly][ShowInInspector] public string Title => _config.Meta.Title;
        [ReadOnly][PreviewField] public Sprite Icon => _config.Meta.Icon;

        public abstract UnityEngine.Object[] Customs { get; }
        public int CurrentIndex => _currentIndex;

        private readonly CustomizationConfig _config;
        private int _currentIndex;

        public event Action<int> Changed;

        public Customization(CustomizationConfig config) => _config = config;

        public void Next()
        {
            _currentIndex++;

            if (_currentIndex >= Customs.Count())
                _currentIndex = 0;

            ApplyCustom(_currentIndex);
            Changed?.Invoke(_currentIndex);
        }

        public void Previous()
        {
            _currentIndex--;

            if (_currentIndex < 0)
                _currentIndex = Customs.Count() - 1;

            ApplyCustom(_currentIndex);
            Changed?.Invoke(_currentIndex);
        }

        public abstract void ApplyCustom(int index);
    }
}