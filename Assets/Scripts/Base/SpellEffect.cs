using UnityEngine;

namespace Arena
{
    public sealed class SpellEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _speed = 2f;

        private Transform _target;
        private float _elapsedTime;

        private float Duration => _particle.main.duration;

        public void SetTarget(Transform target)
        {
            _target = target;
            RotateTowardsTarget();
        }

        private void Update()
        {
            if (_target == null)
                return;

            MoveToTarget();
            DestroyIfEnd();
        }

        private void MoveToTarget() =>
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime / Duration * _speed);

        private void RotateTowardsTarget()
        {
            Vector3 directionToTarget = (_target.position - transform.position).normalized;
            Quaternion rotationToTarget = Quaternion.LookRotation(-directionToTarget);

            rotationToTarget = Quaternion.Euler(0f, rotationToTarget.eulerAngles.y, rotationToTarget.eulerAngles.z);
            transform.rotation = rotationToTarget;
        }

        private void DestroyIfEnd()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= Duration)
                Destroy(gameObject);
        }
    }
}