using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class PlayerRegionsList : DataList<RegionDisplayer>
    {
        [SerializeField] private ListGuidProgress _regionsProgress;

        protected override void AfterAppended(RegionDisplayer reference, string guid)
        {
            //reference.Init(_characterReferences);

            if (_regionsProgress.Contains(guid))
            {
                return;
            }

            SaveProgress(guid);
        }

        private void SaveProgress(string guid)
        {
            _regionsProgress.Add(guid);
            _regionsProgress.Save();
        }
    }
}