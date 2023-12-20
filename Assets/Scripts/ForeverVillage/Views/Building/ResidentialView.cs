using TMPro;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class ResidentialView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gems;
        [SerializeField] private TMP_Text _villagers;
        [SerializeField] private TimerView _timerCanvas;
        [SerializeField] private ButtonCanvas _summonVillagersCanvas;

        private ResidentialBuilding _targetView;
        private VillagersStoreDisplay _villagersStore;

        private readonly Timer _timer = new Timer();

        public void Init(ResidentialBuilding target, VillagersStoreDisplay villagersStoreDisplay)
        {
            _targetView = target;
            _villagersStore = villagersStoreDisplay;
        }

        private void OnEnable()
        {
            _timerCanvas.Init(_timer);

            _targetView.VillagersUpdated += OnVillagersUpdated;
            _targetView.GemsUpdated += OnGemsUpdated;
            _targetView.GemGenerationStarted += OnGemGenerationStarted;

            _summonVillagersCanvas.ButtonClicked += OnSummonVillagerCanvas;
        }

        private void Start()
        {
            _summonVillagersCanvas.Display();
            _timerCanvas.Display();
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        private void OnSummonVillagerCanvas()
        {
            _villagersStore.Display();
            //_villagersStore.OnSmthBuyed += OnBuildZone;
        }

        private void OnGemGenerationStarted(float time)
        {
            _timer.Start(time);
            _timer.Completed += OnCompleted;
        }

        private void OnCompleted()
        {
            _timer.Completed -= OnCompleted;
        }

        private void OnVillagersUpdated(int current, int capacity)
        {
            _villagers.text = GetRatioInText(current, capacity);
        }

        private void OnGemsUpdated(int current, int capacity)
        {
            _gems.text = GetRatioInText(current, capacity);
        }

        private string GetRatioInText(int current, int capacity)
        {
            return $"{current}/{capacity}";
        }

        private void OnDisable()
        {
            _targetView.VillagersUpdated -= OnVillagersUpdated;
            _targetView.GemsUpdated -= OnGemsUpdated;
            _targetView.GemGenerationStarted -= OnGemGenerationStarted;

            _summonVillagersCanvas.ButtonClicked -= OnSummonVillagerCanvas;
        }
    }
}