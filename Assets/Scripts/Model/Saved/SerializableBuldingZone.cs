using System;
using UnityEngine;

[Serializable]
public class SerializableBuldingZone : KeySavedObject<SerializableBuldingZone>
{
    [SerializeField] private BuildingZoneState _currentState;
    [SerializeField] private BuildedBuilding _building;

    public BuildingZoneState State => _currentState;
    public BuildedBuilding Buiding => _building;

    public event Action Destroyed;
    public event Action Cleared;
    public event Action<BuildedBuilding> BuildingLoaded;

    public SerializableBuldingZone(BuildingZoneState state, string guid)
    : base(guid)
    {
        _currentState = state;
    }

    protected override void OnLoad(SerializableBuldingZone loadedObject)
    {
        _currentState = loadedObject._currentState;
        _building = loadedObject._building;

        switch (_currentState)
        {
            case BuildingZoneState.Destroyed:
                Destroyed?.Invoke();
                break;
            case BuildingZoneState.Empty:
                Destroyed?.Invoke();
                Cleared?.Invoke();
                break;
            case BuildingZoneState.Builded:
                Destroyed?.Invoke();
                Cleared?.Invoke();
                LoadBuilding();
                break;
            default:
                break;
        }
    }

    public void Clear()
    {
        Cleared?.Invoke();

        _currentState = BuildingZoneState.Empty;
        Save();
    }

    // when it's build for first (runtime)
    public void ZoneBuilded(string prefabName, string buildedGuid)
    {
        _currentState = BuildingZoneState.Builded;
        _building = new BuildedBuilding(prefabName, buildedGuid);

        _building.Save();
        Save();
    }

    private void LoadBuilding()
    {
        _building.Load();

        BuildingLoaded?.Invoke(_building);
    }
}

[Serializable]
public class BuildedBuilding : KeySavedObject<BuildedBuilding>
{
    // ������� ������ ��� ������

    [SerializeField] private string _prefabName;

    public string Prefab => _prefabName;

    public event Action Builded;

    public BuildedBuilding(string prefabName, string guid)
    : base(guid)
    {
        _prefabName = prefabName;
    }

    protected override void OnLoad(BuildedBuilding loadedObject)
    {
        _prefabName = loadedObject._prefabName;
        // �������� ����������� ������
    }

    public void Instantiate()
    {

    }
}
