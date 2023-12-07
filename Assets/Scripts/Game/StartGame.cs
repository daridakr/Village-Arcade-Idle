using System;
using UnityEngine;

public class StartGame : IntSavedValue
{
    [SerializeField] private InputFieldData _villageNameField;
    [SerializeField] private PlayerBuildingsList _buildingList;
    [SerializeField] private BuildingData[] _buildingsInStart;
    [SerializeField] private PlayerRegionsList _regionList;
    [SerializeField] private RegionData[] _regionsInStart;

    private bool BeginGame = true;

    public bool IsNewGame => Get() == 0;

    public event Action NewGameStarted;
    public event Action GameBegined;
    public event Action<string> VillageNamed;

    private void Start()
    {
        if (IsNewGame)
        {
            NewGameStarted?.Invoke();
            _villageNameField.DataGetted += OnVilageNamed;
        }
    }

    private void OnVilageNamed(string name)
    {
        VillageNamed?.Invoke(name);
        GameBegined?.Invoke();

        AddStartBuildings();
        AddStartRegions();
        Save(Convert.ToInt32(BeginGame));
    }

    private void AddStartBuildings()
    {
        foreach (var building in _buildingsInStart)
        {
            _buildingList.Append(building);
        }
    }

    private void AddStartRegions()
    {
        foreach (var region in _regionsInStart)
        {
            _regionList.Append(region, region.GUID);
        }
    }

    protected override void SetKey()
    {
        Key = SaveKeyParams.Game.New;
    }
}
