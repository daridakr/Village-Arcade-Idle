using System;
using UnityEngine;
using Village.Character;

public class PlayerCharacterModel : MonoBehaviour
{
    protected CustomizableCharacter _instance;

    private CharacterLoader _loader;

    public CustomizableCharacter Character => _instance;
    public event Action Initialized;

    private void OnEnable() => _loader = new CharacterLoader();

    public void Setup(string path)
    {
        if (_instance != null)
            return;

        CustomizableCharacter prefab = _loader.LoadCustomizable(path);
        _instance = Instantiate(prefab, transform);

        OnSetuped();

        Initialized?.Invoke();
    }

    public Animator GetAnimator()
    {
        return _instance.Animator;
    }

    public virtual void UpdateRotation(Vector3 direction)
    {
        _instance.transform.LookAt(_instance.transform.position + direction);
    }

    protected virtual void OnSetuped() { }
}