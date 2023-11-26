using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class CanvasView : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    protected CanvasGroup CanvasGroup => _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public virtual void Display()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public virtual void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }
}
