using Cinemachine;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class PlayerCameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineStateDrivenCamera _cmCamera;

        private PlayerInstanceInfo _player;

        [Inject]
        private void Construct(PlayerInstanceInfo player)
        {
            _player = player;
        }

        private void Awake()
        {
            _cmCamera.Follow = _player.transform;
        }
    }
}