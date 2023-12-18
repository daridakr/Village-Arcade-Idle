using UnityEngine;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(GuidableObject))]
    public class RegionData : MonoBehaviour
    {
        [SerializeField] private GameObject _data;

        private GuidableObject _guidable;

        public string Guid => _guidable.GUID;

        private void Awake()
        {
            _guidable = GetComponent<GuidableObject>();

            Display();
        }

        public void Display()
        {
            _data.SetActive(true);
        }

        public void Hide()
        {
            _data.SetActive(false);
        }
    }
}
