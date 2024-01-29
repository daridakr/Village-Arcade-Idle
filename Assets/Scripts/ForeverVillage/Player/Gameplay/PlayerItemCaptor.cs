using UnityEngine;

namespace Village
{
    public class PlayerItemCaptor : MonoBehaviour
    {
        [SerializeField] private ItemsCollector _itemsCollector;
        [SerializeField] private global::PlayerLevel _playerLevel;
        [SerializeField] private PlayerGems _playerGems;

        private void OnEnable()
        {
            _itemsCollector.ItemCaptured += OnItemCaptured;
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
            _itemsCollector.ItemCaptured -= OnItemCaptured;
        }
    }
}