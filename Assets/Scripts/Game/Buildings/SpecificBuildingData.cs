using System.Collections.Generic;
using UnityEngine;

// Unique data specific to a particular of building.
[CreateAssetMenu(fileName = "SpecificBuilding", menuName = "Buildings/Specific Building", order = 51)]
public class SpecificBuildingData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private int _price;

    public bool IsCorrect => _icons != null && _price > 0 && !string.IsNullOrEmpty(_name);

    public string Name => _name;
    public IEnumerable<Sprite> Icons => _icons;
    public Sprite MainIcon => _icons[0];
    public int Price => _price;
}

//public enum BuildingType
//{
//    RESIDENTIAL,
//    FACTORIES,
//    ATTRACTIONS
//}
