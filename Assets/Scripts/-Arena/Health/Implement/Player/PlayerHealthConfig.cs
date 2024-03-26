using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "PlayerHealthConfig", menuName = "Health/Player Health Config")]
    public sealed class PlayerHealthConfig : HealthConfig
    {
        [SerializeField] private int _armor = 0;

        public int Armor => _armor;

        protected override void Validate()
        {
            base.Validate();

            if (_armor < 0) _armor = 0; 
        }
    }
}