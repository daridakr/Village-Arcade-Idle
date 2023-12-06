using Palmmedia.ReportGenerator.Core.Common;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBuildingsList : ScriptableDataList<BuildingData>
{
    [SerializeField] private ListProgress _buildingsProgress;

    private void Awake()
    {
        if (_buildingsProgress.Data.Count() > 0)
        {
            LoadProgress();
        }
    }

    protected override void AfterAppended(BuildingData reference, string guid)
    {
        //reference.Init(_characterReferences);

        if (_buildingsProgress.Contains(guid))
        {
            return;
        }

        SaveProgress(guid);
    }

    private void LoadProgress()
    {
        JsonSerializer serializer = new JsonSerializer();

        foreach (string guid in _buildingsProgress.Data)
        {
            //BuildingData unlockedData = JsonUtility.FromJson<BuildingData>(guid);
            //BuildingData unlockedData = (BuildingData)serializer.Deserialize(writer);
            //Append(unlockedData, unlockedData.GUID);
        }
    }

    private void SaveProgress(string guid)
    {
        _buildingsProgress.Add(guid);
        _buildingsProgress.Save();
    }
}
