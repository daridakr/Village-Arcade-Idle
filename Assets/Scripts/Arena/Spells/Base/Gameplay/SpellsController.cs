using System;
using System.Collections.Generic;
using UnityEngine;
using Village;
using Zenject;

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

            //foreach (var config in _configs)
            //    config.Init(_factory);

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

        public Spell[] GetAllActiveSpells()
        {
            return _activeSpells.ToArray();
        }
    }
}