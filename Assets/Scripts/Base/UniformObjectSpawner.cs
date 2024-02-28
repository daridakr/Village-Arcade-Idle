using UnityEngine;

public class UniformObjectSpawner
{
    private Transform _parent;
    private Collider _spawnAreaCollider;
    //private Vector3 _spawnAreaSize;

    public UniformObjectSpawner(Collider spawnAreaSize, Transform parent)
    {
        _spawnAreaCollider = spawnAreaSize;
        _parent = parent;
    }

    public GameObject SpawnObject(GameObject prefab)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        spawnPosition.y = prefab.transform.position.y;
        return Object.Instantiate(prefab, spawnPosition, prefab.transform.rotation, _parent);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // �������� ������� ����������
        Bounds bounds = _spawnAreaCollider.bounds;

        // ���������� ��������� ���������� X � Z � �������� ������ ����������
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // �������� ������ ����������
        float colliderHeight = bounds.max.y - bounds.min.y;

        // ������� ��������� ����� ������ ����������
        Vector3 randomPoint = new Vector3(randomX, bounds.min.y + colliderHeight / 2f, randomZ);

        return randomPoint;
    }
}
