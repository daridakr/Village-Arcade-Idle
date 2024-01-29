using UnityEngine;
using Village;
using Village.Character;

public class PlayerCharacterModel : MonoBehaviour
{
    [SerializeField] protected PlayerMovement _movement;

    protected PlayerAnimation _animation;
    protected CustomizableCharacter _instance;

    public CustomizableCharacter Customizable => _instance;

    private CharacterLoader _loader;
    private void OnEnable() => _loader = new CharacterLoader();

    public virtual void Setup(string path)
    {
        CustomizableCharacter prefab = _loader.LoadCustomizable(path);
        _instance = Instantiate(prefab, transform);
        _animation = _instance.GetComponentInChildren<PlayerAnimation>();

        //Debug.Log(transform);
        _movement.Setup(transform, _animation);
    }
}