using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public class StoreSaved : KeySavedObject<StoreSaved>
    {
        [SerializeField] List<Building> _availableBuildings;

        public event Action Added;

        public StoreSaved(string guid)
        : base(guid)
        {

        }

        public void Add(Building building)
        {
            if (building == null)
            {
                throw new ArgumentOutOfRangeException(nameof(building));
            }

            _availableBuildings.Add(building);
            Added?.Invoke();
        }

        protected override void OnLoad(StoreSaved loadedObject)
        {
            //throw new NotImplementedException();
        }
    }
}