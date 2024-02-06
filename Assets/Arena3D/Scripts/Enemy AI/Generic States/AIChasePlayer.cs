using Arena;
using UnityEngine;
using UnityEngine.AI;
using Vampire;
using Zenject;

public class AIChasePlayer : AIState
{
	[SerializeField, Min(0f)] private float ChaseStopDistance = 1.5f;
	[SerializeField, Min(0f)] private float PathUpdateTickTime = 0.25f;

	private NavMeshAgent navMeshAgent;
	private PlayerMovementArena _movingPlayer;

	[Inject]
	private void Construct(PlayerMovementArena movingPlayer)
	{
        _movingPlayer = movingPlayer;
    }

	protected override void Awake()
	{
		base.Awake();
		navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
	}

	public override float GetWeight() => 100f * base.GetWeight();

	public override bool CanEnterState() => Vector3.Distance(stateMachine.transform.position, _movingPlayer.CurrentPosition) > ChaseStopDistance;
	public override bool CanExitState() => true;

	public override void OnEnterState(AIState previousState)
	{
		InvokeRepeating(nameof(UpdatePathToPlayer), Random.Range(0f, PathUpdateTickTime), PathUpdateTickTime);
		navMeshAgent.stoppingDistance = ChaseStopDistance;
		UpdatePathToPlayer();
	}

	private void UpdatePathToPlayer()
	{
		if (Vector3.Distance(stateMachine.transform.position, _movingPlayer.CurrentPosition) <= ChaseStopDistance)
		{
			stateMachine.TransitionTo(stateMachine.GetValidNextState(this));
			return;
		}

		navMeshAgent.SetDestination(_movingPlayer.CurrentPosition);
	}

	public override void OnExitState(AIState nextState)
	{
		CancelInvoke(nameof(UpdatePathToPlayer));
		navMeshAgent.ResetPath();
	}
}
