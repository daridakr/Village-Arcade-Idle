using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(GemsGiver))]
    public class ResidentialBuilding : Building
    {
        // upgradable
        [SerializeField] private int _gemsCapacity = 1;
        [SerializeField] private float _baseGemRate = 0.1f;

        [SerializeField] private int _villagersCapacity = 1;
        [SerializeField] private PlayerTrigger _playerTrigger;
        [SerializeField] private ResidentialView _view;

        private GemsGiver _gemsGiver;
        private Coroutine _gemGenerationRoutine;
        private const int _gemGenerationTimeRate = 30;
        float _currentGemProgress = 0;

        private List<Villager> _villagers = new List<Villager>();

        public int GemCapacity => _gemsCapacity;
        public int VillagerCapacity => _villagersCapacity;

        public event Action<float> GemGenerationStarted;
        public event Action<int, int> GemsUpdated;
        public event Action<int, int> VillagersUpdated; // temp name?

        [Inject]
        public void Construct(VillagersStoreDisplay villagersStore)
        {
            _view.Init(this, villagersStore);
            _view.VillagerSummoned += OnVilalgerSummoned;
        }

        private void Awake()
        {
            _gemsGiver = GetComponent<GemsGiver>();

            _playerTrigger.Enter += OnPlayerTriggerEnter;
        }

        private void Start()
        {
            _upgradeMultiplier = 1; // temp

            VillagersUpdated?.Invoke(_villagers.Count, _villagersCapacity);
            GemsUpdated?.Invoke(0, _gemsCapacity);

            if (_gemGenerationRoutine == null)
            {
                _gemGenerationRoutine = StartCoroutine(StartGenerate());
            }
        }

        private IEnumerator StartGenerate()
        {
            float totalMultiplicator = 1f;

            foreach(Villager villager in _villagers)
            {
                totalMultiplicator += villager.Multiplicator;
            }

            Debug.Log("Total Mult: " + totalMultiplicator);

            float totalGemRate = _baseGemRate * totalMultiplicator;
            Debug.Log("Total Rate: " + totalGemRate);

            float countOfCycles = _gemsCapacity / totalGemRate;
            float totalTime = _gemGenerationTimeRate * Mathf.Ceil(countOfCycles);

            GemGenerationStarted?.Invoke(totalTime);

            float startTime = Time.time;

            do
            {
                float elapsedTime = Time.time - startTime;

                if (elapsedTime >= totalTime)
                {
                    elapsedTime = totalTime; // Заканчиваем, если прошло больше totalTime
                }

                float progressPercentage = elapsedTime / totalTime;
                _currentGemProgress = progressPercentage * _gemsCapacity;

                GemsUpdated?.Invoke((int)_currentGemProgress, _gemsCapacity);

                yield return null;

            } while (_currentGemProgress < _gemsCapacity);

            Debug.Log("Gem generation completed!");
        }

        // Populate(Villager newVillager);

        private void OnPlayerTriggerEnter(Player player)
        {
            if (_currentGemProgress > 0.99f)
            {
                StopCoroutine(_gemGenerationRoutine);
                int generatedGems = Mathf.FloorToInt(_currentGemProgress);
                _gemsGiver.Give(generatedGems);
                _currentGemProgress -= generatedGems;
                GemsUpdated?.Invoke((int)_currentGemProgress, _gemsCapacity);
                _gemGenerationRoutine = StartCoroutine(StartGenerate());
            }
        }

        private void OnVilalgerSummoned(Villager newVillager)
        {
            if (newVillager == null)
            {
                return;
            }

            if (TryPopulate(newVillager))
            {
                VillagersUpdated?.Invoke(_villagers.Count, _villagersCapacity);

                StopCoroutine(_gemGenerationRoutine);
                _gemGenerationRoutine = StartCoroutine(StartGenerate());

                if (_villagers.Count == _villagersCapacity)
                {
                    _view.VillagerSummoned -= OnVilalgerSummoned;
                }
            }
        }

        private bool TryPopulate(Villager newVillager)
        {
            if (_villagers.Count + 1 <= _villagersCapacity)
            {
                _villagers.Add(newVillager);
                return true;
            }

            return false;
        }

        protected override void Upgrade()
        {
            base.Upgrade();

            _gemsCapacity += (int)_upgradeMultiplier;
            //CapacityUpdated?.Invoke(_capacity);
        }

        protected override List<int> InitStats()
        {
            List<int> stats = new List<int>
        {
            _villagersCapacity,
            _gemsCapacity
        };

            return stats;
        }

        private void OnDisable()
        {
            _playerTrigger.Enter -= OnPlayerTriggerEnter;
            _view.VillagerSummoned -= OnVilalgerSummoned;
        }
    }
}