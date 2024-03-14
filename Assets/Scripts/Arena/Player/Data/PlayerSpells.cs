using System;
using UnityEngine;
using Zenject;

namespace Arena
{
    public sealed class PlayerSpells : MonoBehaviour,
        IInitilizable, ICuster
    {
        private ISpellsController _spellsController;
        private Spell[] _activeSpells;

        public Transform Transform => transform;

        //DamageSpell _damagableSpells;   foreach-> damageSpell.Cast(IWeapon);
        //BuffSpells _buffs;    foreach-> buff.StartCasting();

        public event Action Initialized;

        [Inject]
        private void Construct(ISpellsController spellsController) => 
            _spellsController = spellsController;

        public Spell GetMain() => _activeSpells[0];

        private void OnEnable() => _spellsController.Initialized += OnSpellsInitialized;

        private void OnSpellsInitialized()
        {
            _spellsController.Initialized -= OnSpellsInitialized;

            _spellsController.ActivateSpellAtIndex(0);
            _activeSpells = _spellsController.GetAllActiveSpells();

            Initialized?.Invoke();
        }

        public float GetSpellsDamage()
        {
            float damage = 0;

            foreach (var spell in _activeSpells)
            {
                DamageSpell damageSpell = spell as DamageSpell;

                if (damageSpell != null)
                    damage += damageSpell.Damage;
            }

            return damage;
        }

        public void Cust(ITargetsInfo targetsInfo)
        {
            foreach (Spell spell in _activeSpells)
                spell.StartCusting(this, targetsInfo);
        }
    }
}