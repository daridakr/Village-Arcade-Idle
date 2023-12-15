using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GemsGiver))]
public class ResidentialBuilding : Building
{
    // upgradable
    [SerializeField] private int _gemsCapacity = 1;
    [SerializeField] private float _baseGemRate = 0.1f;

    [SerializeField] private int _villagersCapacity = 1;
    private int _currentVillagersCount = 0;

    private float _multiplicator = 2f;
    private int _gemGenerationTimeRate = 30;
    private GemsGiver _gemsGiver;

    //private Villager[] _villagers;
    //list of views of this building

    public int GemCapacity => _gemsCapacity;
    public int VillagerCapacity => _villagersCapacity;
    public float GemRate => _baseGemRate;

    public event Action<int, int> VillagersUpdated;
    public event Action<int, int> GemsUpdated;
    public event Action<float> GemGenerationStarted;
    //public event Action<int> VillagerCapacityUpgraded;

    private void Awake()
    {
        _gemsGiver = GetComponent<GemsGiver>();
    }

    private void Start()
    {
        _upgradeMultiplier = 1;

        VillagersUpdated?.Invoke(_currentVillagersCount, _villagersCapacity);
        GemsUpdated?.Invoke(0, _gemsCapacity);

        StartCoroutine(StartGenerate());
    }

    private IEnumerator StartGenerate()
    {
        float totalGemRate = _baseGemRate * _multiplicator;
        float currentGemProgress = 0;

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

            currentGemProgress = progressPercentage * _gemsCapacity;

            GemsUpdated?.Invoke((int)currentGemProgress, _gemsCapacity);

            yield return null;

        } while (currentGemProgress < _gemsCapacity);

        Debug.Log("Gem generation completed!");
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

    // gemsGiver
    // gemGenerationTimeRate
    // StartGemGeneration()
    // Populate(Villager newVillager)
}
