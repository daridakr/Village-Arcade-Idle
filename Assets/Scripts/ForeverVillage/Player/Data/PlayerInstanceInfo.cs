using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class PlayerInstanceInfo : MonoBehaviour
    {
        [SerializeField] private GameObject _dataComponents;
        [SerializeField] private GameObject _interactorComponents;
        [SerializeField] private GameObject _modelContainer;
        [SerializeField] private PlayerMovement _movement;
        public Vector3 Position => _movement.CurrentPosition;
        public GameObject Data => _dataComponents;
        public GameObject Interactors => _interactorComponents;
        public GameObject Model => _modelContainer;
    }
}