using UnityEngine;
using Village;

namespace ForeverVillage
{
    public class ExperienceCollector : MagnitCollector<DroppableExperiencePoint, CollectableExperience>
    {
        [SerializeField] private PlayerLevelBase _playerLevel;

        protected override void OnCollected(CollectableExperience collectable)
        {
            _playerLevel.TakeExp(collectable.Value);
        }
    }
}