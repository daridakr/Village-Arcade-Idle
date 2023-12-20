using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts
{
    public class BuildingsStoreDisplay : StoreDisplay<Building>
    {
        [SerializeField] private Button _mainTab;
        [SerializeField] private Image _tabFocus;

        private Image _currentTabFocus;

        public override void Display()
        {
            base.Display();

            _mainTab.Select();
            _currentTabFocus = Instantiate(_tabFocus, _mainTab.transform);

            // for tabs allocation
            //if (storable.TryGetComponent(out ResidentialBuilding residential))
            //{
                
            //}
        }

        public void OnTabSelected(Transform tab)
        {
            Destroy(_currentTabFocus.gameObject);
            _currentTabFocus = Instantiate(_tabFocus, tab);
        }

        public override void Hide()
        {
            base.Hide();

            if (_currentTabFocus != null)
            {
                Destroy(_currentTabFocus.gameObject);
            }
        }
    }
}