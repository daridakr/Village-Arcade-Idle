using System;
using System.Collections;
using UnityEngine;

public class GemGenerator : MonoBehaviour
{
    public event Action<float> GemGenerationStarted;
    public event Action<int, int> GemsUpdated;

    private int _baseGemRate = 2;
    private float _multiplicator = 1.5f;
    private int _gemGenerationTimeRate = 10;

    private int _gemsCapacity = 4; // Пример значения Capacity

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

        // Дополнительные действия по завершению генерации
    }

    // Вызов метода
    void Start()
    {
        StartCoroutine(StartGenerate());
    }
}
