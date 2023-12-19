using UnityEngine;
using UnityEngine.Events;

namespace ForeverVillage.Scripts
{
    public abstract class PlayerMoney : MonoBehaviour
    {
        private MoneyBalance _balance;
        private string _saveKey;

        public int Balance => _balance.Value;
        public bool IsEmpty => _balance.Value > 0;

        public event UnityAction<int> BalanceChanged;

        protected virtual void OnEnable()
        {
            _saveKey = GetSaveKey();

            _balance = new MoneyBalance(_saveKey);
            _balance.Load();

            _balance.ValueChanged += OnBalanceChanged;
        }

        private void OnBalanceChanged()
        {
            BalanceChanged?.Invoke(_balance.Value);
            _balance.Save();
        }

        public void Get(int value)
        {
            _balance.Add(value);

            //_totalMoneyProgress.Add(value);
            //_totalMoneyProgress.Save();
        }

        public void Spend(int value)
        {
            _balance.Spend(value);
        }

        public void SetNewBalance(int value)
        {
            _balance.Set(value);
        }

        private void OnDisable()
        {
            _balance.ValueChanged -= OnBalanceChanged;
            _balance.Save();
        }

        protected abstract string GetSaveKey();
    }
}