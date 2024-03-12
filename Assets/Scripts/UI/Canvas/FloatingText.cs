using UnityEngine;

public class FloatingText : TextDisplay
{
    [SerializeField] private float _durationTime;

    private void Start() => Destroy(gameObject, _durationTime);
}
