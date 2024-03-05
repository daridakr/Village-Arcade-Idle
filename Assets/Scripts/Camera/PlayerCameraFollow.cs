using Cinemachine;
using UnityEngine;
using Zenject;

namespace ForeverVillage
{
    public class PlayerCameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cmCamera;

        private PlayerReferenceBase _player;

        [Inject]
        private void Construct(PlayerReferenceBase player)
        {
            _player = player;
        }

        private void Awake()
        {
            _cmCamera.Follow = _player.transform;
        }
    }
}