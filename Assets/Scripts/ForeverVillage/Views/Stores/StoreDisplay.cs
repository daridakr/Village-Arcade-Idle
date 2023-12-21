using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace ForeverVillage.Scripts
{
    public abstract class StoreDisplay<T> : CanvasAnimatedView where T : IStorableObject
    {
        [SerializeField] private Store<T> _store;
        [SerializeField] private StoreItemView<T> _storeItemView;
        [SerializeField] private PlayerMoney _moneyOwner;
        [SerializeField] private VerticalLayoutGroup _content;

        private List<StoreItemView<T>> _storeViews = new List<StoreItemView<T>>();

        public event Action<T> Buyed;

        public override void Display()
        {
            base.Display();
            ClearOldData();

            foreach (T storable in _store.Data)
            {
                StoreItemView<T> storeItemView = Instantiate(_storeItemView, _content.transform);

                storeItemView.Render(storable, _moneyOwner.Balance);

                storeItemView.Buyed += OnBuyed;
                _storeViews.Add(storeItemView);
            }
        }

        private void OnBuyed(T buyed)
        {
            Hide();
            Buyed?.Invoke(buyed);
        }

        private void ClearOldData()
        {
            foreach (StoreItemView<T> storeItemView in _storeViews)
            {
                storeItemView.Buyed -= OnBuyed;
                Destroy(storeItemView.gameObject);
            }

            _storeViews.Clear();
        }
    }
}