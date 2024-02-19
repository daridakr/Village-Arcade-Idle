using Village.Character;

public sealed class SpecializationModelSetuper
{
    private readonly ICustomizableModel _model;
    private readonly CharacterLoader _loader;

    public SpecializationModelSetuper(ICustomizableModel model)
    {
        _model = model;
        _loader = new CharacterLoader();
    }

    public void Setup(string path)
    {
        CustomizableCharacter prefab = _loader.LoadCustomizable(path);
        _model.Create(prefab);
    }
}