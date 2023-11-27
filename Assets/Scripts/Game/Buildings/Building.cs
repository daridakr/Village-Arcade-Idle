using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingData _data;
    [SerializeField] private PlayerMovementTrigger _playerMovementTrigger;
    [SerializeField] private Transform _playerEnterPoint;

    public BuildingData Data => _data;

    private void OnEnable()
    {
        _playerMovementTrigger.Stay += OnPlayerTriggerStay;
    }

    //public void Init(BuildingData data)
    //{
    //    if (data.IsCorrect && _data == null)
    //    {
    //        _data = data;
    //    }
    //}

    private void OnPlayerTriggerStay(PlayerMovement playerMovement)
    {
        playerMovement.MoveToTarget(_playerEnterPoint);
    }

    private void OnDisable()
    {
        _playerMovementTrigger.Stay -= OnPlayerTriggerStay;
    }
}
