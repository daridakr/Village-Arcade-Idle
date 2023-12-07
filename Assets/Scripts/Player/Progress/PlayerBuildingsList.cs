using UnityEngine;

public class PlayerBuildingsList : DataList<BuildingData>
{
    [SerializeField] private ListSOProgress _buildingsProgress;

    protected override void AfterAppended(BuildingData reference)
    {
        //reference.Init(_characterReferences);

        if (_buildingsProgress.Contains(reference))
        {
            return;
        }

        SaveProgress(reference);
    }

    private void SaveProgress(BuildingData reference)
    {
        _buildingsProgress.Add(reference);
        _buildingsProgress.Save();
    }
}
