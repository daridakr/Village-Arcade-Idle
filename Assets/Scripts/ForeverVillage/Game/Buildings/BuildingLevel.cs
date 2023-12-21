using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class BuildingLevel : MonoBehaviour, IStorableObject
    {
        [SerializeField] private int _coinsPrice;
        [SerializeField] private int _gemsPrice = 0;

        private string _levelName;
        private string _levelDescription;
        private Sprite _icon;

        public void Init(string name, Sprite icon, string description = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (icon == null)
            {
                throw new ArgumentNullException(nameof(icon));
            }

            _levelName = name;
            _icon = icon;
            _levelDescription = description;
        }

        public string Name => _levelName;

        public string Description => _levelDescription;

        public int Price => _coinsPrice;
        public int GemsPrice => _gemsPrice;

        public Sprite Icon => _icon;
    }
}