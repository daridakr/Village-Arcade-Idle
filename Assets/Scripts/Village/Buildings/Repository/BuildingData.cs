using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public class BuildingData : KeySavedObject<BuildingData>
    {
        // сделать модель для уровня

        [SerializeField] private string _prefabName;

        public string Prefab => _prefabName;

        public event Action Builded;

        public BuildingData(string prefabName, string guid)
        : base(guid)
        {
            _prefabName = prefabName;
        }

        protected override void OnLoad(BuildingData loadedObject)
        {
            _prefabName = loadedObject._prefabName;
            // передать необходимые данные
        }

        public void Instantiate()
        {

        }
    }
}