using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class CanvasView : MonoBehaviour
{
    [SerializeField] private Animation _appearanceAnimation;

    private CanvasGroup _canvasGroup;
    private FadeAnimation _fadeAnimation;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _fadeAnimation = new FadeAnimation(_canvasGroup);
    }

    public virtual void Display()
    {
        _canvasGroup.alpha = 1;
        _appearanceAnimation.Play();
        _fadeAnimation.Enable();
    }

    public virtual void Hide()
    {
        _fadeAnimation.Disable();
        _canvasGroup.alpha = 0;
    }
}
