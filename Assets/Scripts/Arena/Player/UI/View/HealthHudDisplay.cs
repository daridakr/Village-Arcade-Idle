using UnityEngine;
using UnityEngine.UI;

namespace Vampire.Player
{
    public sealed class HealthHudDisplay : CanvasAnimatedView
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private Slider _healthSlider;

        private void OnEnable()
        {
            Display();

            _health.Changed += UpdateDisplay;
        }

        public void UpdateDisplay(float normalazedValue) => _healthSlider.value = normalazedValue;

        private void Start() => _healthSlider.value = _health.ValueNormalazed;

        private void OnDisable() => _health.Changed -= UpdateDisplay;
    }
}