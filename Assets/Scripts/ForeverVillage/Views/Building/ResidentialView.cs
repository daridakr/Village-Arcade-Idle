using TMPro;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class ResidentialView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gems;
        [SerializeField] private TMP_Text _villagers;
        [SerializeField] private TimerView _timerCanvas;
        [SerializeField] private ButtonCanvas _summonVillagersCanvas;

        private ResidentialBuilding _residential;
        private VillagersStoreDisplay _villagersStore;
        private readonly Timer _timer = new Timer();

        public void Init(ResidentialBuilding residential, VillagersStoreDisplay villagersStoreDisplay )
        {
            _residential = residential;
            _villagersStore = villagersStoreDisplay;
        }

        private void OnEnable()
        {
            _timerCanvas.Init(_timer);

            _summonVillagersCanvas.ButtonClicked += OnAddVillagerCanvas;

            _residential.VillagersUpdated += OnVillagersUpdated;
            _residential.GemsUpdated += OnGemsUpdated;
            _residential.GemGenerationStarted += OnGemGenerationStarted;
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

        private void OnAddVillagerCanvas()
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
            _summonVillagersCanvas.ButtonClicked -= OnAddVillagerCanvas;

            _residential.VillagersUpdated -= OnVillagersUpdated;
            _residential.GemsUpdated -= OnGemsUpdated;
        }
    }
}