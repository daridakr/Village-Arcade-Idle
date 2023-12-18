using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(GuidableObject))]
    public abstract class Building : MonoBehaviour
    {
        [SerializeField] private SpecificBuildingData _specificData;
        [SerializeField] private BuildingTypeData _typeData;
        [SerializeField] private BuildingLevel[] _levels;

        [SerializeField] protected int _countInOneRegion;
        [SerializeField] protected int _maxLevel;

        private List<int> _stats;

        //need unique guid for building and also guid for builded building
        private GuidableObject _guidable;

        private int _levelNumber = 0;
        private BuildingLevel _currentLevel;
        protected float _upgradeMultiplier;
        // unlock type (blueprint, quest, level)

        public SpecificBuildingData SpecificData => _specificData;
        public BuildingTypeData TypeData => _typeData;

        public string Guid => _guidable.GUID;

        public event Action Upgraded;
        public event Action Builded;

        private void OnEnable()
        {
            _guidable = GetComponent<GuidableObject>();
            _guidable.RegenerateGUID();

            _stats = InitStats();
            InitLevels();
        }

        private void InitLevels()
        {
            _currentLevel = _levels[_levelNumber];

            foreach (var level in _levels)
            {
                level.gameObject.SetActive(level != _currentLevel ? false : true);
            }
        }

        // virtual?
        public virtual void Upgrade()
        {
            _levelNumber++;
            Upgraded?.Invoke();
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public Dictionary<Sprite, int> GetStatesWithIcons()
        {
            if (!gameObject.activeInHierarchy)
            {
                _stats = InitStats();
            }

            if (_stats.Count != TypeData.StatIcons.Count())
            {
                return null;
            }

            var statsWithIcons = new Dictionary<Sprite, int>();
            int statNumber = 0;

            foreach (var icon in TypeData.StatIcons)
            {
                statsWithIcons.Add(icon, _stats[statNumber]);
                statNumber++;
            }

            return statsWithIcons;
        }

        protected abstract List<int> InitStats();
    }

}