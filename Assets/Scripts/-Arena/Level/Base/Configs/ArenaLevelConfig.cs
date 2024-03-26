using UnityEngine;
using static Vampire.LevelBlueprint;
using Vampire;

namespace Arena
{
    [CreateAssetMenu(fileName = "NewArenaLevel", menuName = "Arena/Level", order = 1)]
    public class ArenaLevelConfig : ScriptableObject
    {
        [Header("Time")]
        [SerializeField] private float _levelTime = 600;

        [Header("Monster Settings")]
        public MonstersContainer[] _monsters;
        public MiniBossContainer[] _miniBosses;
        public BossContainer _finalBoss;
        public MonsterSpawnTable _monsterSpawnTable;

        [Header("Chest Settings")]
        public ChestBlueprint chestBlueprint;
        public float chestSpawnDelay = 30;
        public float chestSpawnAmount = 2;

        [Header("Exp Gem Settings")]
        public int initialExpGemCount = 25;
        public GemType initialExpGemType = GemType.White1;
    }
}