using ForeverVillage.Scripts.Player;
using ForeverVillage.Scripts.Upgrades.Player;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private IControlService _controlService;

        private float _speed;
        private float _speedRate;
        private float _flySpeedRate;

        private Transform _playerModel;
        private PlayerAnimation _animation;
        private Rigidbody _rigidbody;

        private MovementSpeedUpgrade _upgrade;

        public bool IsMoving { get; private set; }
        public Vector3 CurrentPosition => transform.position;

        [Inject]
        private void Construct(
            IControlService controlService,
            MovementConfig config,
            MovementSpeedUpgrade upgrade)
        {
            _controlService = controlService;
            _controlService.OnMove += Move;
            _controlService.OnStand += Stop;

            _speedRate = config.SpeedRate;
            _flySpeedRate = config.FlySpeedRate;

            _upgrade = upgrade;
            _upgrade.Updated += SetSpeed;
        }

        public void Setup(Transform model, PlayerAnimation animation)
        {
            _playerModel = model;
            _animation = animation;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetSpeed(float value)
        {
            if (value <= 0f)
            {
                return;
            }

            _speed = value;
        }

        public void Move(Vector3 direction)
        {
            _playerModel.LookAt(_playerModel.position + direction);
            _rigidbody.velocity = direction * _speed * _speedRate * _flySpeedRate;
            _animation.SetSpeed(direction.magnitude);

            IsMoving = true;
        }

        public void MoveToTarget(Transform target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
        }

        public void Stop()
        {
            if (IsMoving)
            {
                if (_rigidbody != null)
                {
                    _rigidbody.velocity = Vector3.zero;
                }

                _animation.SetSpeed(0);
                IsMoving = false;
            }
        }

        private void OnDestroy()
        {
            _controlService.OnMove -= Move;
            _controlService.OnStand -= Stop;
            _upgrade.Updated -= SetSpeed;
        }
    }
}