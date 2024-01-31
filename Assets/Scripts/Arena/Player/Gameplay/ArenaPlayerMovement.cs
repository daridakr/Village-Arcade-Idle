using UnityEngine;

namespace Vampire
{
    public sealed class ArenaPlayerMovement : PlayerMovement
    {
        public Vector3 Velocity { get => _rigidbody.velocity; }

        public void UpdateMoveSpeed()
        {
            //_rigidbody.drag = characterBlueprint.acceleration / (movementSpeed.Value * movementSpeed.Value);
        }
    }
}