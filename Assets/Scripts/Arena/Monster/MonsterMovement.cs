using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Arena
{
    [RequireComponent(typeof(MonsterPhysicStateControl))]
    public class MonsterMovement : MonoBehaviour,
        IKnockbackable
    {
        [SerializeField] private float _stillDelay = 1f;
        [Range(0.001f, 0.1f)][SerializeField] private float _stillThreshold = 0.05f;
        [SerializeField] private float _maxKnockbackTime = 0.5f;
        [SerializeField] private MonsterPlayerSensor _sensor;

        private MonsterPhysicStateControl _physicsControl;
        private Transform _playerPosition;

        private Coroutine _moveCoroutine;
        private Coroutine _slowCoroutine;

        private static NavMeshTriangulation _triangulation;

        public event Action<bool> OnMove;

        private void OnEnable()
        {
            _sensor.OnPlayerEnter += OnSeePlayer;
            _sensor.OnPlayerExit += OnLosePlayer;
        }

        private void Awake()
        {
            _physicsControl = GetComponent<MonsterPhysicStateControl>();
            _physicsControl.Initialize();

            if (_triangulation.vertices == null || _triangulation.vertices.Length == 0)
                _triangulation = NavMesh.CalculateTriangulation();

            _moveCoroutine = StartCoroutine(Roam());
        }

        private void Update() => OnMove?.Invoke(_physicsControl.AgentVelocityMagnitude > 0.01f);

        private IEnumerator Roam()
        {
            WaitForSeconds wait = new WaitForSeconds(_stillDelay);

            while (enabled)
            {
                int index = UnityEngine.Random.Range(1, _triangulation.vertices.Length);

                _physicsControl.SetAgentDestination(
                    Vector3.Lerp(
                        _triangulation.vertices[index - 1],
                        _triangulation.vertices[index],
                        UnityEngine.Random.value
                    )
                );

                yield return new WaitUntil(() => _physicsControl.RemainingDistance <= _physicsControl.StoppingDistance);
                yield return wait;
            }
        }

        public void StopMoving()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _physicsControl.StopAgent();
            _physicsControl.DisablePhysics();
        }

        //public void Slow(AnimationCurve slowCurve)
        //{
        //    if (_slowCoroutine != null)
        //        StopCoroutine(_slowCoroutine);

        //    _slowCoroutine = StartCoroutine(SlowDown(slowCurve));
        //}

        //private IEnumerator SlowDown(AnimationCurve slowCurve)
        //{
        //    float time = 0;

        //    while (time < slowCurve.keys[^1].time)
        //    {
        //        _agent.speed = _baseSpeed * slowCurve.Evaluate(time);
        //        time += Time.deltaTime;

        //        yield return null;
        //    }

        //    _agent.speed = _baseSpeed;
        //}

        public void OnSeePlayer(Transform playerTransform)
        {
            if (_physicsControl.IsAgentEnabled)
            {
                StopCoroutine(_moveCoroutine);
                _playerPosition = playerTransform;
                _moveCoroutine = StartCoroutine(ChasePlayer(playerTransform));
            }
        }

        public void OnLosePlayer()
        {
            if (_physicsControl.IsAgentEnabled)
            {
                StopCoroutine(_moveCoroutine);
                _playerPosition = null;
                _moveCoroutine = StartCoroutine(Roam());
            }
        }

        private IEnumerator ChasePlayer(Transform player)
        {
            while (true)
            {
                if (_physicsControl.IsAgentEnabled)
                    _physicsControl.SetAgentDestination(player.position);

                yield return new WaitForSeconds(0.125f);
            }
        }

        public void Knockback(Vector3 force)
        {
            if (_physicsControl.IsAgentEnabled)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = StartCoroutine(ApplyKnockback(force));
            }
        }

        private IEnumerator ApplyKnockback(Vector3 force)
        {
            yield return null;

            _physicsControl.EnableBody();
            _physicsControl.AddBodyForce(force);

            yield return new WaitForFixedUpdate();
            float knockbackTime = Time.time;

            yield return new WaitUntil(
                () => _physicsControl.BodyVelocityMagnitude < _stillThreshold || Time.time > knockbackTime + _maxKnockbackTime
            );

            yield return new WaitForSeconds(0.25f);

            _physicsControl.DisableBody();
            _physicsControl.AgentWarp(transform.position);

            yield return null;

            if (_playerPosition != null)
                _moveCoroutine = StartCoroutine(ChasePlayer(_playerPosition));
            else
                _moveCoroutine = StartCoroutine(Roam());
        }
    }
}