using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public sealed class SpellsController : MonoBehaviour,
        ISpellsController,
        ISpellsSetupper
    {
        private SpellConfig[] _configs;
        private List<Spell> _activeSpells;

        public event Action Initialized;

        public void Setup(SpellsCatalog spellCatalog)
        {
            _configs = spellCatalog.GetAllSpells();

            _activeSpells = new List<Spell>();

            Initialized?.Invoke();
        }

        public void ActivateSpellAtIndex(int index)
        {
            if (_configs == null)
                return;

            var spell = _configs[index].InstantiateSpell();
            _activeSpells.Add(spell);
        }

        public Spell[] GetAllActiveSpells() => _activeSpells.ToArray();
    }
}