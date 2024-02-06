using UnityEngine;

namespace Arena
{
    public sealed class PlayerMovementArena : PlayerMovement
    {
        public Vector3 Velocity { get => _rigidbody.velocity; }

        public void UpdateMoveSpeed()
        {
            //_rigidbody.drag = characterBlueprint.acceleration / (movementSpeed.Value * movementSpeed.Value);
        }
    }
}