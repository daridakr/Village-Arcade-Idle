using ForeverVillage;
using UnityEngine;
using Vampire;

namespace Arena
{
    public sealed class DeadState : State
    {
        [SerializeField] private ExperiencePointDropper _experienceDropper;
        [SerializeField] private int _countOfExpPoints = 1;
        [SerializeField] private CoinsDropper _coinsDropper;
        [SerializeField] private int _maxCountOfCoins = 1;

        private void OnEnable()
        {
            _experienceDropper.Give(_countOfExpPoints);
            _coinsDropper.Give(_maxCountOfCoins);
        }
    }
}