using UnityEngine;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(PlayerWallet))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerName _name;
        [SerializeField] private PlayerLevel _level;
        [SerializeField] private PlayerCoins _coins;
        [SerializeField] private PlayerGems _gems;
        [SerializeField] private PlayerBuildingsList _buildings;
        [SerializeField] private PlayerVillagersList _villagers;
        [SerializeField] private PlayerRegionsList _regions;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerCharacterModel _model;
        [SerializeField] private PlayerTimerCleaner _cleaner;
        [SerializeField] private PlayerTimerBuilder _builder;

        private PlayerWallet _wallet;

        public PlayerName Name => _name;
        public PlayerCharacterModel Model => _model;
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
        public Vector3 Position => _movement.CurrentPosition;

        private void Awake()
        {
            _wallet = GetComponent<PlayerWallet>();
        }
    }
}