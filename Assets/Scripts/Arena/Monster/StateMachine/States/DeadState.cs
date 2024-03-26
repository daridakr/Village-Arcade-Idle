using UnityEngine;
using Village;

namespace Arena
{
    public sealed class DeadState : State
    {
        [SerializeField] private ExperiencePointDropper _experienceDropper;
        [SerializeField] private int _countOfExpPoints = 1;

        private void OnEnable() => _experienceDropper.Give(_countOfExpPoints);
    }
}