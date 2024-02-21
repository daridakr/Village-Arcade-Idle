using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class PlayerTargetTracker : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterModelArena _model;

        public void UpdateTargetRotation(Vector3 playerDirection)
        {
            //ITargetable nearestTarget = FindNearestTarget();

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
    }
}