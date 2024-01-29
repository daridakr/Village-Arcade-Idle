using System;
using TMPro;
using UnityEngine;

namespace Village
{
    public class ResidentialView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gems;
        [SerializeField] private TMP_Text _villagers;
        [SerializeField] private TimerView _timerCanvas;
        [SerializeField] private ButtonCanvas _summonVillagersCanvas;

        private ResidentialBuilding _targetToView;
        private VillagersStoreDisplay _villagersStore;

        private readonly Timer _timer = new Timer();

        public Action<Villager> VillagerSummoned;

        public void Init(ResidentialBuilding target, VillagersStoreDisplay villagersStoreDisplay)
        {
            _targetToView = target;
            _villagersStore = villagersStoreDisplay;
        }

        private void OnEnable()
        {
            _timerCanvas.Init(_timer);

            _targetToView.VillagersUpdated += OnVillagersUpdated;
            _targetToView.GemsUpdated += OnGemsUpdated;
            _targetToView.GemGenerationStarted += OnGemGenerationStarted;

            _summonVillagersCanvas.ButtonClicked += OnSummonVillagerButtonClicked;
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        private void OnGemGenerationStarted(float time)
        {
            _timerCanvas.Display();
            _timer.Start(time);
            _timer.Completed += OnCompleted;
        }

        private void OnCompleted()
        {
            _timer.Completed -= OnCompleted;
        }

        private void OnVillagersUpdated(int current, int capacity)
        {
            _summonVillagersCanvas.Display(current < capacity);
            _villagers.text = GetRatioInText(current, capacity);
        }

        private void OnGemsUpdated(int current, int capacity)
        {
            _gems.text = GetRatioInText(current, capacity);
        }

        private void OnSummonVillagerButtonClicked()
        {
            _villagersStore.Display();
            _villagersStore.Buyed += OnVillagerBuyed;
        }

        private void OnVillagerBuyed(Villager villager)
        {
            _villagersStore.Buyed -= OnVillagerBuyed;

            VillagerSummoned?.Invoke(villager);
        }

        private string GetRatioInText(int current, int capacity)
        {
            return $"{current}/{capacity}";
        }

        private void OnDisable()
        {
            _targetToView.VillagersUpdated -= OnVillagersUpdated;
            _targetToView.GemsUpdated -= OnGemsUpdated;
            _targetToView.GemGenerationStarted -= OnGemGenerationStarted;

            _summonVillagersCanvas.ButtonClicked -= OnSummonVillagerButtonClicked;
        }
    }
}