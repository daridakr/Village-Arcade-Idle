using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerCameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cmCamera;

    private PlayerReference _player;

    [Inject]
    private void Construct(PlayerReference player)
    {
        _player = player;
    }

    private void Awake()
    {
        _cmCamera.Follow = _player.transform;
    }
}