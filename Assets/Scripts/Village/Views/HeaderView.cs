using UnityEngine;

namespace Village
{
    public class HeaderView : CanvasView
    {
        [SerializeField] private GameStarter _start;

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