using UnityEngine;

public class RegionData : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private GameObject _data;

    public int Price => _price;
    public bool IsFree => _price == 0;

    private void Awake()
    {
        Display();
    }

    public void Display()
    {
        _data.SetActive(true);
    }

    public void Hide()
    {
        _data.SetActive(false);
    }
}
