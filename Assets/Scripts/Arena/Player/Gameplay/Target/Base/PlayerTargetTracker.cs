using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class PlayerTargetTracker : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterModelArena _model;
        [SerializeField] private float _targetRadius = 10f;

        private List<ITargetable> _targets;

        private void Start()
        {
            _targets = new List<ITargetable>();
            SearchTargetsAround();
        }

        public void SearchTargetsAround()
        {
            Collider[] colliders = Physics.OverlapSphere(_model.CenterTransform.position, _targetRadius);

            foreach (Collider collider in colliders)
            {
                ITargetable target = collider.GetComponent<ITargetable>();

                if (target != null)
                {
                    //AddTarget(target);
                }
            }
        }

        public void UpdateTargetRotation(Vector3 playerDirection)
        {
            ITargetable nearestTarget = FindNearestTarget();

            //if (nearestTarget != null)
            //{
            //    Vector3 targetDirection = nearestTarget.GetPosition() - _playerTransform.position;
            //    _playerTransform.LookAt(_playerTransform.position + targetDirection);
            //}
            //else
            //{
            //    _playerTransform.LookAt(_playerTransform.position + playerDirection);
            //}
        }

        private ITargetable FindNearestTarget()
        {
            float minDistance = Mathf.Infinity;
            ITargetable nearestTarget = null;

            //foreach (ITargetable target in _targets)
            //{
            //    float distance = Vector3.Distance(_playerTransform.position, target.GetPosition());
            //    if (distance < minDistance)
            //    {
            //        minDistance = distance;
            //        nearestTarget = target;
            //    }
            //}

            return nearestTarget;
        }
    }
}