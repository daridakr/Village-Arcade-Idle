using System;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class BuildingZoneView : MonoBehaviour
    {
        [SerializeField] private MessageDisplayCanvas _clearButton;
        [SerializeField] private ButtonCanvas _buildButton;

        private CanvasAnimatedView _currentCanvas;
        private BuildingsStoreDisplay _buildingsStoreDisplay;

        public event Action CanClear;
        public event Action<Building> CanBuild;

        [Inject]
        public void Construct(BuildingsStoreDisplay buildingsStore)
        {
            _buildingsStoreDisplay = buildingsStore;
        }

        private void OnEnable()
        {
            _clearButton.ButtonClicked += OnClearButtonClicked;
            _buildButton.ButtonClicked += ShowBuildings;
        }

        public void ShowClearButton(int clearPrice, int balance)
        {
            _clearButton.Display(clearPrice, balance >= clearPrice);
            _currentCanvas = _clearButton;
        }

        public void ShowBuildButton()
        {
            _buildButton.Display();
            _currentCanvas = _buildButton;
        }

        public void HideView()
        {
            if (_currentCanvas != null) 
            {
                _currentCanvas.Hide();
            }
        }

        private void OnClearButtonClicked()
        {
            _clearButton.Hide();
            CanClear?.Invoke();
        }

        private void ShowBuildings()
        {
            _buildButton.Hide();
            _buildingsStoreDisplay.Display();
            _buildingsStoreDisplay.Buyed += OnBuyed;
        }

        private void OnBuyed(Building building)
        {
            _buildingsStoreDisplay.Buyed -= OnBuyed;

            CanBuild?.Invoke(building);
        }

        private void OnDisable()
        {
            _clearButton.ButtonClicked -= OnClearButtonClicked;
            _buildButton.ButtonClicked -= ShowBuildings;
        }
    }
}