using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public sealed class AttackAIState : AIState
    {
        [SerializeField] private DamageSpellConfig Spell;
        [SerializeField, Min(0f)] private float Weight = 1000f;

        //private ICaster caster;
        private readonly List<Coroutine> CurrentCastingCoroutines = new();

        public override event Action<AIState[]> CanTransit;
        public event Action Attacked;

        private void Awake()
        {
            //caster = GetComponent<ICaster>();
            //Spell = Spell.Clone();
        }

        //public override float GetWeight() => Weight;

        public override void OnEnterState(AIState previousState) => Attack();

        private bool IsCurrentlyCasting() => CurrentCastingCoroutines.Count != 0;

        private void Attack()
        {
            Debug.Log("Attack");
            Attacked?.Invoke();

            //Spell.PreCast(caster);

            //caster.StatsHandler.GetStat<StatWithCurrentValue>(StatID._Mana).ModifyCurrentValue(-caster.StatsHandler.GetStat<Stat>(StatID._Mana_Cost).GetValue(Spell.ManaCost));
            //float castDuration = 1f / caster.StatsHandler.GetStat<Stat>(StatID._Casting_Speed).GetValue(1f / Spell.CastDuration);
            //foreach (var (normalizedDelay, action) in Spell.SkillCastCallbacks)
            //{
            //    CurrentCastingCoroutines.Add(this.DelayedCallBack(() => action.Invoke(caster, Player.Instance.transform.position), normalizedDelay * castDuration));
            //}

            //CurrentCastingCoroutines.Add(this.DelayedCallBack(ResetSelfAndExitState, castDuration));
            //CurrentCastingCoroutines.Add(this.DelayedCallBack(() => Spell.StartChargeCooldown(caster), castDuration));
        }

        private void ResetSelfAndExitState()
        {
            CurrentCastingCoroutines.Clear();
            //_stateMachine.TransitionTo(_stateMachine.GetValidNextState(this));
        }

        public override bool CanEnterState() => !IsCurrentlyCasting() /*&& Spell.CanCast(caster) && Spell.CanAICast(caster)*/;
        public override bool CanExitState() => !IsCurrentlyCasting();
    }
}