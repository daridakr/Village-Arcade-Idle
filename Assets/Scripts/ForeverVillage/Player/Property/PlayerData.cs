using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private ItemsCollector _collector;
        [SerializeField] private PlayerLevel _playerLevel;
        [SerializeField] private PlayerGems _playerGems;

        private void OnEnable()
        {
            _collector.ItemCaptured += OnItemCaptured;
        }

        private void OnItemCaptured(Item item)
        {
            if (item.TryGetComponent(out ExperiencePoint expPoint))
            {
                _playerLevel.TakeExp(expPoint.Experience);
            }
            else if (item.TryGetComponent(out Gem gem))
            {
                _playerGems.Get(gem.Value);
            }
        }

        private void OnDisable()
        {
            _collector.ItemCaptured -= OnItemCaptured;
        }
    }
}