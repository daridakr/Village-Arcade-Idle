using UnityEngine;

[RequireComponent (typeof(Collider))]
public class BuildingRenderer : MonoBehaviour
{
    [SerializeField] private PlayerMovementTrigger _playerMovementTrigger;
    [SerializeField] private Transform _playerEnterPoint;

    private void OnEnable()
    {
        _playerMovementTrigger.Stay += OnPlayerTriggerStay;
    }

    private void OnPlayerTriggerStay(PlayerMovement playerMovement)
    {
        playerMovement.MoveToTarget(_playerEnterPoint);
    }

    private void OnDisable()
    {
        _playerMovementTrigger.Stay -= OnPlayerTriggerStay;
    }
}
