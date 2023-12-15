using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Buildings/New Building Data", order = 51)]
public class BuildingData : ScriptableObject
{
    //[SerializeField] private Building _building;
    [SerializeField] private string _name;
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private int _price;
    // type of building

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
