using System;
using UnityEngine;

namespace Arena
{
    public sealed class IdleAIState : AIState
    {
        [SerializeField] private MonsterCharacterModel _characterModel;
        [SerializeField] private bool _rotateToFacePlayer = true;
        [SerializeField] private float _rotationSpeed = 10f;

        private Transform _player;

        public override event Action<AIState[]> CanTransit;

        public override float GetWeight() => 0.01f;

        private void Construct(PlayerMovement player) => _player = player.transform;

        public override void WhileInState()
        {
            if (!_rotateToFacePlayer)
                return;

            // need to do physics
            Quaternion playerRotation = Quaternion.LookRotation(_player.position - transform.position, transform.up);

            Quaternion rotation = Quaternion.Slerp(
                _characterModel.transform.rotation,
                playerRotation,
                _rotationSpeed * Time.deltaTime);

            _characterModel.transform.rotation = rotation;
        }

        public override bool CanEnterState() => true;
        public override bool CanExitState() => true;
    }
}