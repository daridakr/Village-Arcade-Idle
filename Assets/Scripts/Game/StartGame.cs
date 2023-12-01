using System;
using UnityEngine;

public class StartGame : IntSavedValue
{
    [SerializeField] private InputFieldData _villageNameField;
    [SerializeField] private PlayerBuildingsList _buildingList;
    [SerializeField] private BuildingData[] _buildingsInStart;

    private bool BeginGame = true;

    public bool IsNewGame => Get() == 0;

    public event Action NewGameStarted;
    public event Action GameBegined;
    public event Action<string> VillageNamed;

    private void Start()
    {
        // temp
        AddStartBuildings();

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

        //AddStartBuildings();
        Save(Convert.ToInt32(BeginGame));
    }

    private void AddStartBuildings()
    {
        foreach (var building in _buildingsInStart)
        {
            _buildingList.UnlockData(building, building.Renderer.GUID);
        }
    }

    protected override void SetKey()
    {
        Key = SaveKeyParams.Game.New;
    }
}
