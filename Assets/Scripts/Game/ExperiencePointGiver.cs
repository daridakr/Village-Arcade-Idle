using System.Collections;
using UnityEngine;

public class ExperiencePointGiver : MonoBehaviour, IExperiencePointGiver
{
    [SerializeField] private DroppableExperiencePoint _expPointPrefab;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(0.38f, 2.2f, 0.0f);
    [SerializeField] private float _prePushDelay = 0.1f;

    private const float _pushForce = 10f;
    private const float _pushHeightExtra = 5f;
    private const float _spawnBetweenDelay = 0.1f;

    private const int _count = 5;

    private Vector3 _spawnPosition => transform.position + _spawnOffset + Random.insideUnitSphere * 0.5f;

    public void Give(int multiplier = 1)
    {
        StartCoroutine(CreateExperiencePoint(multiplier * _count));
    }

    private IEnumerator CreateExperiencePoint(int count)
    {
        if (_prePushDelay != 0)
            yield return new WaitForSeconds(_prePushDelay);

        for (int i = 0; i < count; i++)
        {
            DroppableExperiencePoint spawnedExpPoint = Instantiate(_expPointPrefab, _spawnPosition, Random.rotation);
            spawnedExpPoint.Push(GetRandomDirection() * _pushForce);

            if (_spawnBetweenDelay != 0)
                yield return new WaitForSeconds(_spawnBetweenDelay);
        }

        //PayCompleted?.Invoke();
    }

    private Vector3 GetRandomDirection()
    {
        var direction = Random.insideUnitSphere;
        direction.y = Mathf.Abs(direction.y);
        direction.y += _pushHeightExtra;

        return direction.normalized;
    }
}
