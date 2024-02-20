using UnityEngine;

public interface IAnimatedModel : IInitilizable
{
    public Animator GetAnimator();
    public Transform HeadRig { get; }
    public Transform HandRig { get; }
}