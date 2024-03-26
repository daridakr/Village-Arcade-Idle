using UnityEngine;

namespace Arena
{
    public sealed class DamageDisplay : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private FloatingText _floatingText;

        private void OnEnable() => _health.Damaged += CreateDamageText;

        private void CreateDamageText(float damage)
        {
            FloatingText damageText = Instantiate(_floatingText, transform.position, Quaternion.identity, transform);
            damageText.Display(damage.ToString());
        }

        private void OnDisable() => _health.Damaged -= CreateDamageText;
    }
}