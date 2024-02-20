using Village.Character;

public interface ICustomizableModel
{
    public void Create(CustomizableCharacter prefab);
    public ICustomizableCharacter Customizable { get; }
}