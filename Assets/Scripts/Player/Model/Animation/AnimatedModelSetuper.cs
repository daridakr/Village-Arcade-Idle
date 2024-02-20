using Zenject;

public sealed class AnimatedModelSetuper
    : IInitializable
{
    private readonly PlayerAnimation _animation;
    private readonly IAnimatedModel _model;

    public AnimatedModelSetuper(PlayerAnimation animation, IAnimatedModel model)
    {
        _animation = animation;
        _model = model;
    }

    public void Initialize()
    {
        _animation.InitModel(_model);
    }
}