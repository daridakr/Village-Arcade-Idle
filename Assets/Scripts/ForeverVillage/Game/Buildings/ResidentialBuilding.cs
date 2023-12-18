using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private Coroutine _gemGenerationRoutine;
        float _currentGemProgress = 0;
        private int _currentVillagersCount = 0;

        private float _multiplicator = 2f;
        private const int _gemGenerationTimeRate = 30;
        private GemsGiver _gemsGiver;

        //private Villager[] _villagers;
        //list of views of this building

        public int GemCapacity => _gemsCapacity;
        public int VillagerCapacity => _villagersCapacity;
        public float GemRate => _baseGemRate;

        public event Action<int, int> VillagersUpdated;
        public event Action<int, int> GemsUpdated;
        public event Action<float> GemGenerationStarted;

        private void Awake()
        {
            _gemsGiver = GetComponent<GemsGiver>();
            _playerTrigger.Enter += OnPlayerTriggerEnter;
        }

        private void Start()
        {
            _upgradeMultiplier = 1;

            VillagersUpdated?.Invoke(_currentVillagersCount, _villagersCapacity);
            GemsUpdated?.Invoke(0, _gemsCapacity);

            if (_gemGenerationRoutine == null)
            {
                _gemGenerationRoutine = StartCoroutine(StartGenerate());
            }
        }

        private IEnumerator StartGenerate()
        {
            float totalGemRate = _baseGemRate * _multiplicator;

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

        public override void Upgrade()
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
        }
        // gemsGiver
        // gemGenerationTimeRate
        // StartGemGeneration()
        // Populate(Villager newVillager)
    }
}