using UnityEngine;

public class Building : MonoBehaviour
{
    // should add BuildingData as Scriptable Game Object
    [SerializeField] private string _name;
    [SerializeField] private int _price;

    public string Name => _name;
    public int Price => _price;

    // type of building
}
