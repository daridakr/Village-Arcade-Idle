using UnityEngine;

namespace Village
{
    public class PlayerBuildingsList : DataList<Building>
    {
        [SerializeField] private ListGuidProgress _buildingsProgress;

        protected override void AfterAppended(Building reference, string guid)
        {
            //reference.Init(_characterReferences);

            if (_buildingsProgress.Contains(guid))
            {
                return;
            }

            SaveProgress(guid);
        }

        private void SaveProgress(string guid)
        {
            _buildingsProgress.Add(guid);
            _buildingsProgress.Save();
        }
    }
}