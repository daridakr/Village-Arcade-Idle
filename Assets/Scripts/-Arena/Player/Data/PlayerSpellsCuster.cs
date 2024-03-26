using System;
using UnityEngine;
using Zenject;

namespace Arena
{
    public sealed class PlayerSpellsCuster : MonoBehaviour,
        IInitilizable,
        ICusterWithWeaponDamage
    {
        private ISpellsController _spellsController;
        private Spell[] _activeSpells;

        public Transform Transform => transform;

        public event Action Initialized;

        [Inject]
        private void Construct(ISpellsController spellsController) => 
            _spellsController = spellsController;

        private void OnEnable() => _spellsController.Initialized += OnSpellsInitialized;

        private void OnSpellsInitialized()
        {
            _spellsController.Initialized -= OnSpellsInitialized;

            _spellsController.ActivateSpellAtIndex(0);
            _activeSpells = _spellsController.GetAllActiveSpells();

            Initialized?.Invoke();
        }

        public void Cust(ITargetsInfo targetsInfo, IWeaponDamager weapon)
        {
            foreach (Spell spell in _activeSpells)
                spell.StartCusting(custer: this, targetsInfo, weapon.TotalDamage);
        }

        public Spell GetMain() => _activeSpells[0];
    }
}

// future :
//DamageSpell _damagableSpells;   foreach-> damageSpell.Cast(IWeapon);
//BuffSpells _buffs;    foreach-> buff.StartCasting();

//public float GetTotalSpellsDamage()
//{
//    float damage = 0;

//    foreach (var spell in _activeSpells)
//    {
//        DamageSpell damageSpell = spell as DamageSpell;

//        if (damageSpell != null)
//            damage += damageSpell.Damage;
//    }

//    return damage;
//}