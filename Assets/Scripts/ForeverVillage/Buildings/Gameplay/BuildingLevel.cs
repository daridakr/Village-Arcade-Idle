using System;
using UnityEngine;

namespace Village
{
    [RequireComponent (typeof (Rigidbody))]
    public class BuildingLevel : MonoBehaviour, IStorableObject
    {
        [SerializeField] private int _coinsPrice;
        [SerializeField] private int _gemsPrice = 0;

        private string _levelName;
        private string _levelDescription;
        private Sprite _icon;
        private Rigidbody _body;
        private BuildingLevelType _type;

        private const int _levelTypesAmount = 4; // should relocate

        private void OnEnable()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _body.isKinematic = true;
            gameObject.layer = 0;
        }

        public void Init(int value, Sprite icon, string buildingName = "", string description = "")
        {
            if (value < 0 || value > _levelTypesAmount)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (icon == null)
            {
                throw new ArgumentNullException(nameof(icon));
            }

            _type = (BuildingLevelType)value;
            _icon = icon;
            _levelName = $"{_type} {buildingName}";
            _levelDescription = description;
        }

        public string Name => _levelName;

        public string Description => _levelDescription;

        public int Price => _coinsPrice;
        public int GemsPrice => _gemsPrice;

        public Sprite Icon => _icon;
        public int Value => (int)_type + 1;
    }

    public enum BuildingLevelType
    {
        Common,
        Cozy,
        Charming,
        Elegant,
        Prestigious,
        Exquisite,
        Majestic,
        Legendary
    }
}