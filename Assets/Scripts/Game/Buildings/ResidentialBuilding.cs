using System;
using System.Collections;
using UnityEngine;

public class ResidentialBuilding : Building
{
    [SerializeField] private int _gemsCapacity = 1;
    [SerializeField] private int _villagersCapacity = 1;

    private int _currentVillagersCount = 0;

    private int _baseGemRate = 2;
    private float _multiplicator = 2f;
    private int _gemGenerationTimeRate = 30;

    //private Villager[] _villagers;
    //list of views of this building

    public event Action<int, int> VillagersUpdated;
    public event Action<int, int> GemsUpdated;
    public event Action<float> GemGenerationStarted;
    //public event Action<int> VillagerCapacityUpgraded;

    private void Start()
    {
        _upgradeMultiplier = 1;

        VillagersUpdated?.Invoke(_currentVillagersCount, _villagersCapacity);
        GemsUpdated?.Invoke(0, _gemsCapacity); // 1/1

        StartCoroutine(StartGenerate());
    }

    private IEnumerator StartGenerate()
    {
        float totalGemRate = _baseGemRate * _multiplicator;
        Debug.Log("Total: " + totalGemRate);

        float currentGemProgress = 0;
        Debug.Log("Current: " + currentGemProgress);

        float countOfCycles = _gemsCapacity / totalGemRate;
        float totalTime = _gemGenerationTimeRate * Mathf.Ceil(countOfCycles);
        Debug.Log("Total time: " + totalTime);

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
            Debug.Log("Current: " + currentGemProgress);

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

    // gemsGiver
    // gemGenerationTimeRate
    // StartGemGeneration()
    // Populate(Villager newVillager)
}
