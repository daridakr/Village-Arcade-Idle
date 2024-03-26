using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Arena
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] EnemyPrefabs;
        private ISpawnableEnemy[] SpawnableEnemyPrefabs;

        [SerializeField, Min(1f)] private float ThreatMatchingTimer = 10f;
        [SerializeField, Min(1f)] private float SpawningTimer = 0.25f;

        [SerializeField, Min(0f), Space] private float SpawnRadius = 50f;

        [SerializeField, Min(1f), Space] private float StartingThreatLevel = 5f;
        [SerializeField, Min(0f)] private float ThreatLevelDelta = 0.2f;

        private float currentTargetThreatLevel;
        private float currentThreatLevel => GetCurrentTotalThreatLevel();

        private readonly Queue<ISpawnableEnemy> EnemySpawnQueue = new();

        private PlayerCharacterModelArena _player;
        private IEnemyFactory _enemyFactory;

        public event Action MonsterKillded;

        [Inject]
        private void Construct(PlayerCharacterModelArena player, IEnemyFactory factory)
        {
            _player = player;
            _enemyFactory = factory;
        }

        private void Awake()
        {
            SpawnableEnemyPrefabs = EnemyPrefabs.Select(E => E.GetComponent<ISpawnableEnemy>()).ToArray();

            currentTargetThreatLevel = StartingThreatLevel;

            InvokeRepeating(nameof(UpdateSpawnQueue), 0f, ThreatMatchingTimer);
            InvokeRepeating(nameof(SpawnQueuedEnemy), 0f, SpawningTimer);
        }

        private void Update() => currentTargetThreatLevel += ThreatLevelDelta * Time.deltaTime;

        private float GetCurrentTotalThreatLevel() => NPC.All.Sum(npc => npc.IsAlive ? npc.ThreatLevel : 0f)
            + EnemySpawnQueue.Sum(enemy => enemy.ThreatLevel);

        private void UpdateSpawnQueue()
        {
            do
            {
                float threatDifference = currentTargetThreatLevel - GetCurrentTotalThreatLevel();
                ISpawnableEnemy enemyToSpawn = GetSpawnableEnemyUnderThreatLevel(threatDifference);
                if (enemyToSpawn is null)
                {
                    return;
                }

                EnemySpawnQueue.Enqueue(enemyToSpawn);
            } while (true);
        }

        private ISpawnableEnemy GetSpawnableEnemyUnderThreatLevel(float threatLevel)
        {
            ISpawnableEnemy[] validEnemies = SpawnableEnemyPrefabs.Where(E => 0f < E.ThreatLevel && E.ThreatLevel <= threatLevel).ToArray();

            float totalWeight = validEnemies.Sum(E => E.SpawnWeight);
            float randomWeight = UnityEngine.Random.Range(0f, totalWeight);

            foreach (var item in validEnemies)
            {
                randomWeight -= item.SpawnWeight;
                if (randomWeight <= 0f)
                {
                    return item;
                }
            }

            return null;
        }

        private void SpawnQueuedEnemy()
        {
            if (!EnemySpawnQueue.TryDequeue(out ISpawnableEnemy enemy))
            {
                return;
            }

            Transform newEnemy = _enemyFactory.Create(enemy as NPC).transform;

            //Transform newEnemy = Instantiate(enemy as Component).transform;
            int index = UnityEngine.Random.Range(1, 100);
            newEnemy.name = (enemy as NPC).name + index;

            Vector2 randomPositionOffset = UnityEngine.Random.insideUnitCircle.normalized * SpawnRadius;
            newEnemy.position = transform.position + new Vector3(randomPositionOffset.x, 0f, randomPositionOffset.y);
            newEnemy.LookAt(_player.transform);

            newEnemy.GetComponent<EnemyHealth>().Dead += OnSpawnedMonsterDead;
        }

        private void OnSpawnedMonsterDead(EnemyHealth enemyHealth)
        {
            enemyHealth.Dead -= OnSpawnedMonsterDead;
            MonsterKillded?.Invoke();
        }


#if UNITY_EDITOR
        private void GetAllEnemiesInProject()
        {
            EnemyPrefabs = UnityEditor.AssetDatabase.FindAssets("t:GameObject")
                .Select(guid => UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid)))
                .Where(prefab => prefab.TryGetComponent(out ISpawnableEnemy _))
                .ToArray();
        }
#endif

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, SpawnRadius);
        }
    }
}