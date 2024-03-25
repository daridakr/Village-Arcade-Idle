using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Arena
{
    public class DefeatScreenView : CanvasAnimatedView
    {
        [SerializeField] private float _waitTime;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private ButtonDisplay _againButton;
        [SerializeField] private ButtonDisplay _mainButton;

        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;

        private void OnEnable()
        {
            _playerHealth.Emptied += () => Invoke(nameof(OnPlayerDefeat), _waitTime);

            _againButton.Clicked += OnAgainButtonClicked;
            _mainButton.Clicked += OnMainButtonClicked;
        }

        private void OnPlayerDefeat()
        {
            Display();
            Time.timeScale = 0f;
        }

        private void OnAgainButtonClicked()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnMainButtonClicked()
        {
            Time.timeScale = 1f;
            _sceneLoader.LoadScene("Main");
        }

        private void OnDisable()
        {
            _playerHealth.Emptied -= () => Invoke(nameof(OnPlayerDefeat), _waitTime);

            _againButton.Clicked -= OnAgainButtonClicked;
            _mainButton.Clicked -= OnMainButtonClicked;
        }
    }
}