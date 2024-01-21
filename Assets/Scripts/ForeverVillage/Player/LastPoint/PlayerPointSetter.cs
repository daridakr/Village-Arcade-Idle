using ForeverVillage.Scripts;
using UnityEngine;
using Zenject;

public class PlayerPointSetter :
    IInitializable
{
    private PlayerPointRepository _repository;
    private PlayerMovement _movement;

    private const float _y = 0.5f;

    [Inject]
    public void Construct(PlayerPointRepository repository, PlayerMovement movement)
    {
        _repository = repository;
        _movement = movement;
    }

    public void Initialize()
    {
        if (_repository.Load(out PlayerPointData playerPointData))
        {
            Transform point = _movement.transform;
            point.position = new Vector3(playerPointData.X, _y, playerPointData.Z);

            _movement.MoveToTarget(point);
        }
    }
}