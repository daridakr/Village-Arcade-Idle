using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class Customization :
        ICustomization
    {
        [ReadOnly][ShowInInspector] public string Id => _config.Id;
        [ReadOnly][ShowInInspector] public string Title => _config.Meta.Title;
        [ReadOnly][ShowInInspector] public int Index => _currentIndex;
        [ReadOnly][PreviewField] public Sprite Icon => _config.Meta.Icon;

        public abstract UnityEngine.Object[] Customs { get; }

        private readonly CustomizationConfig _config;
        private int _currentIndex = 0;

        public event Action<int> Changed;

        public Customization(CustomizationConfig config) => _config = config;

        public void Setup(int index)
        {
            if (index <= 0 || index >= Customs.Count())
            {
                throw new ArgumentException($"Index {index} for customization {Title} is invalid. Max allowable index - {Customs.Count()}");
            }

            _currentIndex = index;
        }

        public void Next()
        {
            _currentIndex++;

            if (_currentIndex >= Customs.Count())
                _currentIndex = 0;

            ApplyCustom();
            Changed?.Invoke(_currentIndex);
        }

        public void Previous()
        {
            _currentIndex--;

            if (_currentIndex < 0)
                _currentIndex = Customs.Count() - 1;

            ApplyCustom();
            Changed?.Invoke(_currentIndex);
        }

        public abstract void ApplyCustom();
    }
}