using Arena;
using UnityEngine;
using Vampire;
using Zenject;

[RequireComponent(typeof(AIStateMachine))]
public abstract class AIState : MonoBehaviour
{
	protected AIStateMachine stateMachine;
    protected PlayerMovementArena _target;

    protected virtual void Awake() => stateMachine = GetComponent<AIStateMachine>();

	[Inject]
    private void Construct(PlayerMovementArena target)
	{
        _target = target;
    }

	public abstract bool CanEnterState();
	public abstract bool CanExitState();

	public virtual float GetWeight() => 100f;

	public virtual void OnEnterState(AIState previousState) { }
	public virtual void WhileInState() { }
	public virtual void OnExitState(AIState nextState) { }
}
