using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public sealed class GetPlayerNameView : CanvasAnimatedView
    {
        [SerializeField] private InputFieldData _villageNameInput;

        public event Action<string> Getted;

        private void OnEnable() => _villageNameInput.DataGetted += NameGetted;

        private void NameGetted(string name)
        {
            _villageNameInput.DataGetted -= NameGetted;

            Getted?.Invoke(name);
        }
    }
}