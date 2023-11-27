using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Buildings/New Building Data", order = 51)]
public class BuildingData : ScriptableObject
{
    [SerializeField] private BuildingRenderer _renderer;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    // type of building

    public bool IsCorrect => _icon != null && _renderer != null && _price > 0 && !string.IsNullOrEmpty(_name);

    public string Name => _name;
    public Sprite Icon => _icon;
    public int Price => _price;
    public BuildingRenderer Renderer => _renderer;
}
