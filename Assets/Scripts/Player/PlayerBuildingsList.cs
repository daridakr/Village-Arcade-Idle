using UnityEngine;

public class PlayerBuildingsList : ScriptableObjectList<BuildingData>
{
    [SerializeField] private ListProgressData _buildingsProgress;

    private void OnEnable()
    {
        LoadProgress();
        //UnlockData();
    }

    protected override void AfterUnlocked(BuildingData reference, string guid)
    {
        //reference.Init(_characterReferences);

        if (_buildingsProgress.Contains(guid))
            return;

        SaveProgress(guid);
    }

    private void LoadProgress()
    {
        _buildingsProgress.Load();
    }

    private void SaveProgress(string guid)
    {
        _buildingsProgress.Add(guid);
        _buildingsProgress.Save();
    }
}
