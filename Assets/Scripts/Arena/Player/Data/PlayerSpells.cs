using UnityEngine;
using Zenject;

namespace Arena
{
    public sealed class PlayerSpells : MonoBehaviour
    {
        private ISpellsController _spellsController;

        private Spell[] _activeSpells;

        [Inject]
        private void Construct(ISpellsController spellsController) => 
            _spellsController = spellsController;

        private void OnEnable()
        {
            _spellsController.Initialized += OnSpellsInitialized;
        }

        private void OnSpellsInitialized()
        {
            _spellsController.ActivateSpellAtIndex(0);
            _activeSpells = _spellsController.GetAllActiveSpells();
        }

        public void Activate(ITargetsInfo targetsInfo)
        {
            foreach (Spell spell in _activeSpells)
            {
                spell.StartCusting(targetsInfo);
            }
        }

        private void OnDisable()
        {
            _spellsController.Initialized -= OnSpellsInitialized;
        }
    }
}