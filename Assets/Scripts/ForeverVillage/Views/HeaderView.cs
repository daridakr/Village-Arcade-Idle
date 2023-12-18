using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class HeaderView : CanvasView
    {
        [SerializeField] private StartGame _start;

        private void Start()
        {
            if (_start.IsNewGame)
            {
                _start.GameBegined += OnGameBegined;
            }
            else
            {
                Display();
            }
        }

        private void OnGameBegined()
        {
            _start.GameBegined -= OnGameBegined;

            Display();
        }
    }
}