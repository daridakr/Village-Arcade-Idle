public abstract class CanvasAnimatedView : CanvasView
{
    //[SerializeField] private Animation _appearanceAnimation;

    private FadeAnimation _fadeAnimation;

    public override void Display()
    {
        base.Display();
        GetAnimation();
        //_appearanceAnimation.Play();
        _fadeAnimation.Enable();
    }

    public override void Hide()
    {
        GetAnimation();
        _fadeAnimation.Disable();
        base.Hide();
    }

    private void GetAnimation()
    {
        _fadeAnimation = new FadeAnimation(CanvasGroup);
    }
}
