using System;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public class SavedBuildingZone : KeySavedObject<SavedBuildingZone>
    {
        private SavedBuildingZone(string key)
            : base(key)
        {
        }

        protected override void OnLoad(SavedBuildingZone loadedObject)
        {
            throw new System.NotImplementedException();
        }
    }
}