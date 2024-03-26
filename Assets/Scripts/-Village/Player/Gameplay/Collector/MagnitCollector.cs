using System;
using UnityEngine;
using Village;

namespace ForeverVillage
{
    public abstract class MagnitCollector<D, C> : MonoBehaviour where D : DroppableItem 
        where C : Collectable
    {
        [SerializeField] protected DroppableTrigger<D> _trigger;
        [SerializeField] protected CollectableMagnit _magnit;

        private void OnEnable() => _trigger.Stay += OnStay;

        private void OnStay(D droppable)
        {
            if (droppable.CanCapture == false)
                return;

            C collectable = droppable.Capture() as C;
            OnCollected(collectable);
            _magnit.Attract(collectable);
        }

        protected abstract void OnCollected(C collectable);

        private void OnDisable() => _trigger.Stay -= OnStay;
    }
}