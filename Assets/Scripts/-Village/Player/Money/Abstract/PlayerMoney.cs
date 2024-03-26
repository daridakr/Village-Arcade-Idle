using System;
using UnityEngine;
using UnityEngine.Events;

namespace ForeverVillage
{
    public abstract class PlayerMoney : MonoBehaviour,
        IGameSaveDataListener
    {
        private MoneyBalance _balance;
        private string _saveKey;

        public int Balance => _balance.Value;
        public bool IsEmpty => _balance.Value <= 0;

        public event Action<int> Recieved;
        public event Action<int> Spended;
        public event Action BalanceChanged;

        protected virtual void OnEnable()
        {
            _saveKey = GetSaveKey();

            _balance = new MoneyBalance(_saveKey);
            _balance.Load();

            _balance.ValueChanged += OnBalanceChanged;
        }

        private void OnBalanceChanged()
        {
            BalanceChanged?.Invoke();
            _balance.Save();
        }

        public void Recieve(int value)
        {
            _balance.Add(value);

            Recieved?.Invoke(value);

            //_totalMoneyProgress.Add(value);
            //_totalMoneyProgress.Save();
        }

        public void Spend(int value)
        {
            _balance.Spend(value);

            Spended?.Invoke(value);
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

        public void OnSaveData(GameSaveReason reason)
        {
            _balance.Save();
        }
    }
}