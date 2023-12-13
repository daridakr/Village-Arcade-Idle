using System;
using UnityEngine;

[Serializable]
public class BuildZone : KeySavedObject<BuildZone>
{
    [SerializeField] private BuildingZoneState _currentState; // should remove after save builded buildings implementation and rewrite to like regionZone w events
    [SerializeField] private Builded _building;

    public BuildingZoneState State => _currentState;
    public Builded Buiding => _building;

    public event Action Destroyed;
    public event Action Cleared;
    public event Action Builded;

    public BuildZone(BuildingZoneState state, string guid)
    : base(guid)
    {
        _currentState = state;
    }

    protected override void OnLoad(BuildZone loadedObject)
    {
        _currentState = loadedObject._currentState;

        switch (_currentState)
        {
            case BuildingZoneState.Destroyed:
                Destroyed?.Invoke();
                break;
            case BuildingZoneState.Empty:
                Cleared?.Invoke();
                break;
            case BuildingZoneState.Builded:
                Builded?.Invoke();
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

    public void Build()
    {
        Builded?.Invoke();

        _currentState = BuildingZoneState.Builded;
        Save();
    }
}

[Serializable]
public abstract class Builded : KeySavedObject<Builded>
{
    public Builded(string guid)
: base(guid)
    {

    }

    protected override void OnLoad(Builded loadedObject)
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public abstract class BuildedResidential : Builded
{


    public BuildedResidential(string guid)
: base(guid)
    {

    }
}
