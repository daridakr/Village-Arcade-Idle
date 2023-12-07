using UnityEngine;

public class RegionData : GUIDDataObject
{
    [SerializeField] private GameObject _data;

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
