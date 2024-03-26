using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public sealed class CoinsDropper : Dropper<DroppableCoin>
    {
        [SerializeField][Range(0, 1f)] private float _dropChance;

        private float _randDropChance;
        private float _cumulative = 0f;

        protected override void Drop()
        {
            GenerateRandomDropChance();
            _cumulative += _dropChance;

            if (_randDropChance < _cumulative)
                base.Drop();
        }

        private void GenerateRandomDropChance() => _randDropChance = Random.Range(0f, 1.0f);
    }
}