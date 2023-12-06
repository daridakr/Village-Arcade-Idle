using UnityEngine;

public class ExperiencePointCollector : MonoBehaviour
{
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private DroppableExperiencePointTrigger _trigger;
    [SerializeField] private ItemsMagnit _magnit;

    private void OnEnable()
    {
        _trigger.Stay += OnStay;
    }

    private void OnStay(DroppableExperiencePoint dropExpPoint)
    {
        if (dropExpPoint.CanCapture == false)
            return;

        ExperiencePoint expPoint = dropExpPoint.Capture();
        _playerLevel.TakeExp(expPoint.Experience);

        _magnit.Attract(expPoint);
    }

    private void OnDisable()
    {
        _trigger.Stay -= OnStay;
    }
}
