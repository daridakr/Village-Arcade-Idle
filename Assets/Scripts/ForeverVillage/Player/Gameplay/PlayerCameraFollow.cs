using Cinemachine;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class PlayerCameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineStateDrivenCamera _cmCamera;

        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _cmCamera.Follow = _player.transform;
        }
    }
}