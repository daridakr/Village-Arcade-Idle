using UnityEngine;

public abstract class EntityAttackAnimator : MonoBehaviour
{
	[SerializeField] private Animator Animator;
	[SerializeField, Min(1)] private int AnimationLayerIndex = 1;
	[SerializeField] private int AnimationSpeed;
	[SerializeField] private int BasicAttackTrigger;

	public void SetAnimationSpeed(float speed) => Animator.SetFloat(AnimationSpeed, speed);

	public void StartBasicAttackAnimation() => Animator.SetTrigger(BasicAttackTrigger);

	private void OnEnable() => Animator.SetLayerWeight(AnimationLayerIndex, 1f);
	private void OnDisable() => Animator.SetLayerWeight(AnimationLayerIndex, 0f);
}
