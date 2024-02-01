using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Vampire
{
    /// <summary>
    /// Manager class for monsters, monster-dependent objects such as exp gems and coins, chests etc.
    /// </summary>
    public class EntityManager : MonoBehaviour
    {
        [Header("Monster Spawning Settings")]
        [SerializeField] private float monsterSpawnBufferDistance;  // Extra distance outside of the screen view at which monsters should be spawned
        [SerializeField] private float playerDirectionSpawnWeight;  // How much do we weight the player's movement direction in the spawning of monsters
        [Header("Chest Spawning Settings")]
        [SerializeField] private  float chestSpawnRange = 5;
        [Header("Object Pool Settings")]
        [SerializeField] private GameObject monsterPoolParent;
        private MonsterPool[] monsterPools;
        [SerializeField] private GameObject projectilePoolParent;
        private List<ProjectilePool> projectilePools;
        private Dictionary<GameObject, int> projectileIndexByPrefab;
        [SerializeField] private GameObject throwablePoolParent;
        private List<ThrowablePool> throwablePools;
        private Dictionary<GameObject, int> throwableIndexByPrefab;
        [SerializeField] private GameObject boomerangPoolParent;
        private List<BoomerangPool> boomerangPools;
        private Dictionary<GameObject, int> boomerangIndexByPrefab;
        [SerializeField] private GameObject expGemPrefab;
        [SerializeField] private ExpGemPool expGemPool;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private CoinPool coinPool;
        [SerializeField] private GameObject chestPrefab;
        [SerializeField] private ChestPool chestPool;
        [SerializeField] private GameObject textPrefab;
        [SerializeField] private DamageTextPool textPool;
        [Header("Spatial Hash Grid Settings")]
        [SerializeField] private Vector2 gridSize;
        [SerializeField] private Vector2Int gridDimensions;
        [Header("Dependencies")]
        [SerializeField] private SpriteRenderer flashSpriteRenderer;
        [SerializeField] private Camera playerCamera;  // 攝像頭
        [SerializeField] private Vector2 _xLimits;
        [SerializeField] private Vector2 _zLimits;

        private ArenaPlayerCharacterModel _playerModel;  // 玩家的角色
        private ArenaPlayerMovement _playerMovement;

        private StatsManager statsManager;
        private Inventory inventory;
        private FastList<Monster> livingMonsters;
        private FastList<Collectable> magneticCollectables;
        public FastList<Chest> chests; 
        private float timeSinceLastMonsterSpawned;
        private float timeSinceLastChestSpawned;
        private float screenWidthWorldSpace, screenHeightWorldSpace, screenDiagonalWorldSpace, screenDepthWorldSpace;
        private float minSpawnDistance;
        private Coroutine flashCoroutine;
        private Coroutine shockwave;
        private SpatialHashGrid grid;

        public FastList<Monster> LivingMonsters { get => livingMonsters; }
        public FastList<Collectable> MagneticCollectables { get => magneticCollectables; }
        public Inventory Inventory { get => inventory; }
        public AbilitySelectionDialog AbilitySelectionDialog { get; private set; }
        public SpatialHashGrid Grid { get => grid; }

        [Inject]
        private void Construct(
            ArenaPlayerCharacterModel playerModel,
            ArenaPlayerMovement playerMovement)
        {
            _playerModel = playerModel;
            _playerMovement = playerMovement;
        }

        public void Init(LevelBlueprint levelBlueprint, Inventory inventory, StatsManager statsManager, AbilitySelectionDialog abilitySelectionDialog)
        {
            this.inventory = inventory;
            this.statsManager = statsManager;
            AbilitySelectionDialog = abilitySelectionDialog;

            // Determine the screen size in world space so that we can spawn enemies outside of it
            Vector3 bottomLeftNear = playerCamera.ViewportToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
            Vector3 topRightNear = playerCamera.ViewportToWorldPoint(new Vector3(1, 1, playerCamera.nearClipPlane));
            Vector3 topRightFar = playerCamera.ViewportToWorldPoint(new Vector3(1, 1, playerCamera.farClipPlane));

            screenWidthWorldSpace = topRightNear.x - bottomLeftNear.x;
            screenHeightWorldSpace = topRightNear.y - bottomLeftNear.y;
            screenDepthWorldSpace = topRightFar.z - bottomLeftNear.z; // Определение глубины экрана в трехмерном пространстве
            screenDiagonalWorldSpace = (topRightNear - bottomLeftNear).magnitude;
            minSpawnDistance = screenDiagonalWorldSpace / 2;

            // Init fast lists
            livingMonsters = new FastList<Monster>();
            magneticCollectables = new FastList<Collectable>();
            chests = new FastList<Chest>();
            
            // Initialize a monster pool for each monster prefab
            monsterPools = new MonsterPool[levelBlueprint.monsters.Length + 1];
            for (int i = 0; i < levelBlueprint.monsters.Length; i++)
            {
                monsterPools[i] = monsterPoolParent.AddComponent<MonsterPool>();
                monsterPools[i].InitPlayer(_playerModel, _playerMovement);
                monsterPools[i].Init(this, levelBlueprint.monsters[i].monstersPrefab);
            }
            monsterPools[monsterPools.Length-1] = monsterPoolParent.AddComponent<MonsterPool>();
            monsterPools[monsterPools.Length-1].Init(this, levelBlueprint.finalBoss.bossPrefab);

            // Initialize a projectile pool for each ranged projectile type
            projectileIndexByPrefab = new Dictionary<GameObject, int>();
            projectilePools = new List<ProjectilePool>();
            // Initialize a throwable pool for each throwable type
            throwableIndexByPrefab = new Dictionary<GameObject, int>();
            throwablePools = new List<ThrowablePool>();
            // Initialize a boomerang pool for each boomerang type
            boomerangIndexByPrefab = new Dictionary<GameObject, int>();
            boomerangPools = new List<BoomerangPool>();
            // Initialize remaining one-off object pools
            expGemPool.Init(this, expGemPrefab);
            coinPool.Init(this, coinPrefab);
            chestPool.Init(this, chestPrefab);
            textPool.Init(this, textPrefab);

            // Init spatial hash grid
            Vector3[] bounds = new Vector3[] { (Vector2)_playerModel.transform.position - gridSize/2, (Vector2)_playerModel.transform.position + gridSize/2 };
            grid = new SpatialHashGrid(bounds, gridDimensions);
        }

        void Update()
        {
            // Rebuild the grid if the player gets close to the edge
            if (grid.CloseToEdge(_playerModel))
            {
                grid.Rebuild(_playerModel.transform.position);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Special Functions
        ////////////////////////////////////////////////////////////////////////////////
        public void CollectAllCoinsAndGems()
        {
            //if (shockwave != null) StopCoroutine(shockwave);
            //shockwave = StartCoroutine(infiniteBackground.Shockwave(screenDiagonalWorldSpace/2));
            foreach (Collectable collectable in magneticCollectables.ToList())
            {
                collectable.Collect();
            }
        }

        public void DamageAllVisibileEnemies(float damage)
        {
            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(Flash());
            foreach (Monster monster in livingMonsters.ToList() )
            {
                if (TransformOnScreen(monster.transform, Vector2.one))
                    monster.TakeDamage(damage, Vector2.zero);
            }
        }

        public void KillAllMonsters()
        {
            foreach (Monster monster in livingMonsters.ToList() )
            {
                if (!(monster as BossMonster))
                    StartCoroutine(monster.Killed(false));
            }
        }

        private IEnumerator Flash()
        {
            flashSpriteRenderer.enabled = true;
            float t = 0;
            while (t < 1)
            {
                flashSpriteRenderer.color = new Color(1, 1, 1, 1-EasingUtils.EaseOutQuart(t));
                t += Time.unscaledDeltaTime * 4;
                yield return null;
            }
            flashSpriteRenderer.enabled = false;
        }

        public bool TransformOnScreen(Transform transform, Vector2 buffer = default(Vector2))
        {
            return (
                transform.position.x > _playerModel.transform.position.x - screenWidthWorldSpace/2 - buffer.x &&
                transform.position.x < _playerModel.transform.position.x + screenWidthWorldSpace/2 + buffer.x &&
                transform.position.y > _playerModel.transform.position.y - screenHeightWorldSpace/2 - buffer.y &&
                transform.position.y < _playerModel.transform.position.y + screenHeightWorldSpace/2 + buffer.y
            );
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Monster Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public Monster SpawnMonsterRandomPosition(int monsterPoolIndex, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            // Find a random position offscreen


            //Vector3 spawnPosition = (_playerMovement.Velocity != Vector3.zero) ? GetRandomMonsterSpawnPositionPlayerVelocity() : GetRandomMonsterSpawnPosition();

            Vector3 spawnPosition = GetRandomMonsterSpawnPosition();

            // Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            // Vector2 spawnPosition = (Vector2)playerCharacter.transform.position + spawnDirection * (minSpawnDistance + monsterSpawnBufferDistance);

            // Spawn the monster
            return SpawnMonster(monsterPoolIndex, spawnPosition, monsterBlueprint, hpBuff);
        }

        public Monster SpawnMonster(int monsterPoolIndex, Vector3 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            Monster newMonster = monsterPools[monsterPoolIndex].Get();
            newMonster.Setup(monsterPoolIndex, position, monsterBlueprint, hpBuff);
            grid.InsertClient(newMonster);
            return newMonster;
        }

        public void DespawnMonster(int monsterPoolIndex, Monster monster, bool killedByPlayer = true)
        {
            if (killedByPlayer)
            {
                statsManager.IncrementMonstersKilled();
            }
            grid.RemoveClient(monster);
            monsterPools[monsterPoolIndex].Release(monster);
        }

        private Vector3 GetRandomMonsterSpawnPosition()
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(_xLimits.x, _xLimits.y), // случайная координата x в заданных пределах
                4f,
                Random.Range(_zLimits.x, _zLimits.y)  // случайная координата z в заданных пределах
            );

            return spawnPosition;
        }

        //private Vector3 GetRandomMonsterSpawnPosition()
        //{
        //    Vector3[] sideDirections = new Vector3[] { Vector3.left, Vector3.up, Vector3.right, Vector3.down, Vector3.forward, Vector3.back }; // Добавляем направления вперед и назад для трехмерного пространства
        //    int sideIndex = Random.Range(0, 5);
        //    Vector3 spawnPosition;

        //    float halfScreenWidth = screenWidthWorldSpace / 2;
        //    float halfScreenHeight = screenHeightWorldSpace / 2;
        //    float halfScreenDepth = screenDepthWorldSpace / 2; // Добавляем глубину экрана в трехмерном пространстве

        //    if (sideIndex % 2 == 0)
        //    {
        //        spawnPosition = _playerModel.transform.position +
        //            sideDirections[sideIndex] * (halfScreenWidth + monsterSpawnBufferDistance) +
        //            Vector3.up * Random.Range(-halfScreenHeight - monsterSpawnBufferDistance, halfScreenHeight + monsterSpawnBufferDistance) +
        //            Vector3.forward * Random.Range(-halfScreenDepth - monsterSpawnBufferDistance, halfScreenDepth + monsterSpawnBufferDistance); // Добавляем случайную глубину спавна в трехмерном пространстве
        //    }
        //    else
        //    {
        //        spawnPosition = _playerModel.transform.position +
        //            sideDirections[sideIndex] * (halfScreenHeight + monsterSpawnBufferDistance) +
        //            Vector3.right * Random.Range(-halfScreenWidth - monsterSpawnBufferDistance, halfScreenWidth + monsterSpawnBufferDistance) +
        //            Vector3.forward * Random.Range(-halfScreenDepth - monsterSpawnBufferDistance, halfScreenDepth + monsterSpawnBufferDistance); // Добавляем случайную глубину спавна в трехмерном пространстве
        //    }

        //    return spawnPosition;
        //}

        private Vector3 GetRandomMonsterSpawnPositionPlayerVelocity()
        {
            Vector3[] sideDirections = new Vector3[] { Vector3.left, Vector3.up, Vector3.right, Vector3.down, Vector3.forward, Vector3.back }; // Добавляем направления вперед и назад для трехмерного пространства

            float[] sideWeights = new float[]
            {
                Vector3.Dot(_playerMovement.Velocity.normalized, sideDirections[0]),
                Vector3.Dot(_playerMovement.Velocity.normalized, sideDirections[1]),
                Vector3.Dot(_playerMovement.Velocity.normalized, sideDirections[2]),
                Vector3.Dot(_playerMovement.Velocity.normalized, sideDirections[3]),
                Vector3.Dot(_playerMovement.Velocity.normalized, sideDirections[4]),
                Vector3.Dot(_playerMovement.Velocity.normalized, sideDirections[5])
            };

            float extraWeight = sideWeights.Sum() / playerDirectionSpawnWeight;
            int badSideCount = sideWeights.Count(x => x <= 0);

            for (int i = 0; i < sideWeights.Length; i++)
            {
                if (sideWeights[i] <= 0)
                    sideWeights[i] = extraWeight / badSideCount;
            }

            float totalSideWeight = sideWeights.Sum();
            float rand = Random.Range(0f, totalSideWeight);
            float cumulative = 0;
            int sideIndex = -1;

            for (int i = 0; i < sideWeights.Length; i++)
            {
                cumulative += sideWeights[i];
                if (rand < cumulative)
                {
                    sideIndex = i;
                    break;
                }
            }

            Vector3 spawnPosition;

            float halfScreenWidth = screenWidthWorldSpace / 2;
            float halfScreenHeight = screenHeightWorldSpace / 2;
            float halfScreenDepth = screenDepthWorldSpace / 2; // Добавляем переменную для половины глубины пространства

            if (sideIndex % 2 == 0)
            {
                spawnPosition = _playerModel.transform.position +
                    sideDirections[sideIndex] * (halfScreenWidth + monsterSpawnBufferDistance) +
                    Vector3.up * Random.Range(-halfScreenHeight - monsterSpawnBufferDistance, halfScreenHeight + monsterSpawnBufferDistance) +
                    Vector3.forward * Random.Range(-halfScreenDepth - monsterSpawnBufferDistance, halfScreenDepth + monsterSpawnBufferDistance); // Учитываем глубину при создании точки спавна
            }
            else
            {
                spawnPosition = _playerModel.transform.position +
                    sideDirections[sideIndex] * (halfScreenHeight + monsterSpawnBufferDistance) +
                    Vector3.right * Random.Range(-halfScreenWidth - monsterSpawnBufferDistance, halfScreenWidth + monsterSpawnBufferDistance) +
                    Vector3.forward * Random.Range(-halfScreenDepth - monsterSpawnBufferDistance, halfScreenDepth + monsterSpawnBufferDistance); // Учитываем глубину при создании точки спавна
            }

            return spawnPosition;
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Exp Gem Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public ExpGem SpawnExpGem(Vector2 position, GemType gemType = GemType.White1, bool spawnAnimation = true)
        {
            ExpGem newGem = expGemPool.Get();
            newGem.Setup(position, gemType, spawnAnimation);
            return newGem;
        }

        public void DespawnGem(ExpGem gem)
        {
            expGemPool.Release(gem);
        }

        public void SpawnGemsAroundPlayer(int gemCount, GemType gemType = GemType.White1)
        {
            for (int i = 0; i < gemCount; i++)
            {
                Vector2 spawnDirection = Random.insideUnitCircle.normalized;
                Vector2 spawnPosition = (Vector2)_playerModel.transform.position + spawnDirection * Mathf.Sqrt(Random.Range(1, Mathf.Pow(minSpawnDistance, 2)));
                SpawnExpGem(spawnPosition, gemType, false);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Coin Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public Coin SpawnCoin(Vector2 position, CoinType coinType = CoinType.Bronze1, bool spawnAnimation = true)
        {
            Coin newCoin = coinPool.Get();
            newCoin.Setup(position, coinType, spawnAnimation);
            return newCoin;
        }

        public void DespawnCoin(Coin coin, bool pickedUpByPlayer = true)
        {
            if (pickedUpByPlayer)
            {
                statsManager.IncreaseCoinsGained((int)coin.CoinType);
            }
            coinPool.Release(coin);
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Chest Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public Chest SpawnChest(ChestBlueprint chestBlueprint)
        {
            Chest newChest = chestPool.Get();
            newChest.Setup(chestBlueprint);
            // Ensure the chest is not spawned on top of another chest
            bool overlapsOtherChest = false;
            int tries = 0;
            do
            {
                Vector2 spawnDirection = Random.insideUnitCircle.normalized;
                Vector2 spawnPosition = (Vector2)_playerModel.transform.position + spawnDirection * (minSpawnDistance + monsterSpawnBufferDistance + Random.Range(0, chestSpawnRange));
                newChest.transform.position = spawnPosition;
                overlapsOtherChest = false;
                foreach (Chest chest in chests)
                {
                    if (Vector2.Distance(chest.transform.position, spawnPosition) < 0.5f)
                    {
                        overlapsOtherChest = true;
                        break;
                    }
                }
            } while (overlapsOtherChest && tries++ < 100);
            chests.Add(newChest);
            return newChest;
        }

        public Chest SpawnChest(ChestBlueprint chestBlueprint, Vector2 position)
        {
            Chest newChest = chestPool.Get();
            newChest.transform.position = position;
            newChest.Setup(chestBlueprint);
            chests.Add(newChest);
            return newChest;
        }

        public void DespawnChest(Chest chest)
        {
            chests.Remove(chest);
            chestPool.Release(chest);
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Text Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public DamageText SpawnDamageText(Vector2 position, float damage)
        {
            DamageText newText = textPool.Get();
            newText.Setup(position, damage);
            return newText;
        }

        public void DespawnDamageText(DamageText text)
        {
            textPool.Release(text);
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Projectile Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public Projectile SpawnProjectile(int projectileIndex, Vector2 position, float damage, float knockback, float speed, LayerMask targetLayer)
        {
            Projectile projectile = projectilePools[projectileIndex].Get();
            projectile.Setup(projectileIndex, position, damage, knockback, speed, targetLayer);
            return projectile;
        }
        
        public void DespawnProjectile(int projectileIndex, Projectile projectile)
        {
            projectilePools[projectileIndex].Release(projectile);
        }

        public int AddPoolForProjectile(GameObject projectilePrefab)
        {
            if (!projectileIndexByPrefab.ContainsKey(projectilePrefab))
            {
                projectileIndexByPrefab[projectilePrefab] = projectilePools.Count;
                ProjectilePool projectilePool = projectilePoolParent.AddComponent<ProjectilePool>();
                projectilePool.Init(this, projectilePrefab);
                projectilePools.Add(projectilePool);
                return projectilePools.Count - 1;
            }
            return projectileIndexByPrefab[projectilePrefab];
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Throwable Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public Throwable SpawnThrowable(int throwableIndex, Vector2 position, float damage, float knockback, float speed, LayerMask targetLayer)
        {
            Throwable throwable = throwablePools[throwableIndex].Get();
            throwable.Setup(throwableIndex, position, damage, knockback, speed, targetLayer);
            return throwable;
        }

        public void DespawnThrowable(int throwableIndex, Throwable throwable)
        {
            throwablePools[throwableIndex].Release(throwable);
        }

        public int AddPoolForThrowable(GameObject throwablePrefab)
        {
            if (!throwableIndexByPrefab.ContainsKey(throwablePrefab))
            {
                throwableIndexByPrefab[throwablePrefab] = throwablePools.Count;
                ThrowablePool throwablePool = throwablePoolParent.AddComponent<ThrowablePool>();
                throwablePool.Init(this, throwablePrefab);
                throwablePools.Add(throwablePool);
                return throwablePools.Count - 1;
            }
            return throwableIndexByPrefab[throwablePrefab];
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// Boomerang Spawning
        ////////////////////////////////////////////////////////////////////////////////
        public Boomerang SpawnBoomerang(int boomerangIndex, Vector2 position, float damage, float knockback, float throwDistance, float throwTime, LayerMask targetLayer)
        {
            Boomerang boomerang = boomerangPools[boomerangIndex].Get();
            boomerang.Setup(boomerangIndex, position, damage, knockback, throwDistance, throwTime, targetLayer);
            return boomerang;
        }

        public void DespawnBoomerang(int boomerangIndex, Boomerang boomerang)
        {
            boomerangPools[boomerangIndex].Release(boomerang);
        }

        public int AddPoolForBoomerang(GameObject boomerangPrefab)
        {
            if (!boomerangIndexByPrefab.ContainsKey(boomerangPrefab))
            {
                boomerangIndexByPrefab[boomerangPrefab] = boomerangPools.Count;
                BoomerangPool boomerangPool = boomerangPoolParent.AddComponent<BoomerangPool>();
                boomerangPool.Init(this, boomerangPrefab);
                boomerangPools.Add(boomerangPool);
                return boomerangPools.Count - 1;
            }
            return boomerangIndexByPrefab[boomerangPrefab];
        }
    }
}
