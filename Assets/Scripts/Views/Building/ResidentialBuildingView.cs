using TMPro;
using UnityEngine;

public class ResidentialBuildingView : MonoBehaviour
{
    [SerializeField] private ResidentialBuilding _residential;
    [SerializeField] private ButtonCanvas _villagersCanvas;
    [SerializeField] private TMP_Text _villagers;
    [SerializeField] private TimerView _timerCanvas;
    [SerializeField] private TMP_Text _gems;

    private readonly Timer _timer = new Timer();

    private void OnEnable()
    {
        _timerCanvas.Init(_timer);

        _villagersCanvas.ButtonClicked += OnAddVillagerCanvas;

        _residential.VillagersUpdated += OnVillagersUpdated;
        _residential.GemsUpdated += OnGemsUpdated;
        _residential.GemGenerationStarted += OnGemGenerationStarted;
    }

    private void Start()
    {
        _villagersCanvas.Display();
        _timerCanvas.Display();
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    private void OnAddVillagerCanvas()
    {

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
        _villagersCanvas.ButtonClicked -= OnAddVillagerCanvas;

        _residential.VillagersUpdated -= OnVillagersUpdated;
        _residential.GemsUpdated -= OnGemsUpdated;
    }
}
