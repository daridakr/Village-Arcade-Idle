using UnityEngine;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerWallet))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerTimerCleaner _cleaner;
        [SerializeField] private PlayerTimerBuilder _builder;
        [SerializeField] private PlayerLevel _level;
        [SerializeField] private PlayerCoins _coins;
        [SerializeField] private PlayerGems _gems;
        [SerializeField] private PlayerBuildingsList _buildings;
        [SerializeField] private PlayerVillagersList _villagers;
        [SerializeField] private PlayerRegionsList _regions;

        private PlayerMovement _movement;
        private PlayerWallet _wallet;

        public PlayerMovement Movement => _movement;
        public PlayerTimerCleaner Cleaner => _cleaner;
        public PlayerTimerBuilder Builder => _builder;
        public PlayerLevel Level => _level;
        public PlayerWallet Wallet => _wallet;
        public PlayerCoins Coins => _coins;
        public PlayerGems Gems => _gems;
        public PlayerBuildingsList Buildings => _buildings;
        public PlayerVillagersList Villagers => _villagers;
        public PlayerRegionsList Regions => _regions;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _wallet = GetComponent<PlayerWallet>();
        }
    }
}