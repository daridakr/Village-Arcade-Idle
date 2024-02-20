using Village.Character;

public sealed class SpecializationModelInitiator
{
    private readonly ICustomizableModel _model;
    private readonly CharacterLoader _loader;

    public SpecializationModelInitiator(ICustomizableModel model)
    {
        _model = model;
        _loader = new CharacterLoader();
    }

    public void Init(string path)
    {
        CustomizableCharacter prefab = _loader.LoadCustomizable(path);
        _model.Create(prefab);
    }
}