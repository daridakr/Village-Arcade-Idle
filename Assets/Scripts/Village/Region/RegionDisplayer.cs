using UnityEngine;

namespace Village
{
    [RequireComponent(typeof(GuidableObject))]
    public class RegionDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _dataToDisplay;

        private GuidableObject _guidable;

        public string Guid => _guidable.GUID;

        private void Awake()
        {
            _guidable = GetComponent<GuidableObject>();

            Display();
        }

        public void Display()
        {
            foreach (var item in _dataToDisplay)
            {
                item.SetActive(true);
            }
        }

        public void Hide()
        {
            foreach (var item in _dataToDisplay)
            {
                item.SetActive(false);
            }
        }
    }
}
