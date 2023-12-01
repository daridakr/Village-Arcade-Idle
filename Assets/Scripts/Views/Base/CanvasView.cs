using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class CanvasView : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    protected CanvasGroup CanvasGroup => _canvasGroup;

    public bool IsVisible { get; private set; }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public virtual void Display()
    {
        IsVisible = true;
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public virtual void Hide()
    {
        IsVisible = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }
}
