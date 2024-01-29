using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public class BuldingZoneData : KeySavedObject<BuldingZoneData>
    {
        [SerializeField] private BuildingZoneState _currentState;
        [SerializeField] private BuildingData _building;

        public BuildingZoneState State => _currentState;
        public BuildingData Buiding => _building;

        public event Action Destroyed;
        public event Action Cleared;
        public event Action<BuildingData> BuildingLoaded;

        public BuldingZoneData(BuildingZoneState state, string guid)
        : base(guid)
        {
            _currentState = state;
        }

        protected override void OnLoad(BuldingZoneData loadedObject)
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
            _building = new BuildingData(prefabName, buildedGuid);

            _building.Save();
            Save();
        }

        private void LoadBuilding()
        {
            _building.Load();

            BuildingLoaded?.Invoke(_building);
        }
    }
}