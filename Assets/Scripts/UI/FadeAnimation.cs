using DG.Tweening;
using UnityEngine;

public class FadeAnimation
{
    private readonly CanvasGroup _canvasGroup;
    private Tweener _fadeTweener;
    private float _duration = 0.23f;

    public FadeAnimation(CanvasGroup canvasGroup)
    {
        _canvasGroup = canvasGroup;
    }

    public void Enable()
    {
        _canvasGroup.interactable = true;
        Fade(1, _duration);
    }

    public void Disable()
    {
        _canvasGroup.interactable = false;
        Fade(0, _duration);
    }

    private void Fade(float endValue, float duration)
    {
        if (_fadeTweener.IsActive())
            _fadeTweener.Kill();

        _fadeTweener = _canvasGroup.DOFade(endValue, duration);
    }
}
