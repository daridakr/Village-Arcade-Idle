using UnityEngine;
using Village;

namespace Arena
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private TargetDetector _targetDetector;
        // надо предоставить 

        private void OnEnable()
        {
            _targetDetector.OnNoneTarget += StopAttack;
        }

        private void StartAttack(ITargetable target)
        {
            if (target == null)
                return;

            //_specialization.ApplySpells(); - smth like that
        }

        private void StopAttack()
        {

        }

        private void OnDisable()
        {
            _targetDetector.OnNoneTarget -= StopAttack;
        }
    }
}