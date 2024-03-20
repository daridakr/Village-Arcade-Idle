using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private int _frameRate = 60;
    [SerializeField] private TMP_Text _textDisplay;

    private float _deltaTime = 0.0f;

    private void Start() => Application.targetFrameRate = _frameRate;

    private void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _textDisplay.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}