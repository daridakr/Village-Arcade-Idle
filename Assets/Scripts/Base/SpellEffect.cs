using UnityEngine;

namespace Arena
{
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class SpellEffect : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        private ParticleSystem _particle;

        private Transform _target;
        private float _elapsedTime;

        private float Duration => _particle.main.duration;

        private void Awake() => _particle = GetComponent<ParticleSystem>();

        public void SetTarget(Transform target) => _target = target;

        private void Update()
        {
            if (_target == null)
                return;

            MoveToTarget();
            DestroyIfEnd();
        }

        private void MoveToTarget() =>
            transform.position = Vector3.Lerp(transform.position, _target.transform.position, Time.deltaTime / Duration * _speed);

        private void DestroyIfEnd()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= Duration)
                Destroy(gameObject);
        }
    }
}