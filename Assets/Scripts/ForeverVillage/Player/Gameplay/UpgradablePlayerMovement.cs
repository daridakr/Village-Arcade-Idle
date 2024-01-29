using UnityEngine;
using Village.Upgrades.Player;
using Zenject;

namespace Village
{
    public sealed class UpgradablePlayerMovement : PlayerMovement
    {
        private MovementSpeedUpgrade _upgrade;

        public Vector3 CurrentPosition => transform.position;

        [Inject]
        private void Construct(MovementSpeedUpgrade upgrade)
        {
            _upgrade = upgrade;
            _upgrade.Updated += SetSpeed;
        }

        public void SetSpeed(float value)
        {
            if (value <= 0f)
            {
                return;
            }

            _speed = value;
        }

        protected override void OnDestroyed() => _upgrade.Updated -= SetSpeed;
    }
}