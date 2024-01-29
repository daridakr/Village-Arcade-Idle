using System;
using UnityEngine;

namespace Village
{
    public class ItemsCollector : MonoBehaviour
    {
        [SerializeField] private DroppableItemTrigger _trigger;
        [SerializeField] private ItemsMagnit _magnit;

        public event Action<Item> ItemCaptured;

        private void OnEnable()
        {
            _trigger.Stay += OnStay;
        }

        private void OnStay(DroppableItem droppable)
        {
            if (droppable.CanCapture == false)
                return;

            Item item = droppable.Capture();
            ItemCaptured?.Invoke(item);

            _magnit.Attract(item);
        }

        private void OnDisable()
        {
            _trigger.Stay -= OnStay;
        }
    }
}