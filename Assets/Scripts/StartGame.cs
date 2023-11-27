using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private MoneyOwner _playerMoney;
    [SerializeField] private PlayerBuildingsList _buildingList;
    [SerializeField] private BuildingData[] _buildingsInStart;

    private void Start()
    {
        // temp
        _playerMoney.AddMoney(1000);
        AddStartBuildings();

        //if (PlayerPrefs.GetInt("First") == 0)
        //{
        //    PlayerPrefs.SetInt("First", 1);
        //    AddStartBuildings();
        //}
    }

    private void AddStartBuildings()
    {
        foreach (var building in _buildingsInStart)
        {
            _buildingList.UnlockData(building, building.Renderer.GUID);
        }
    }
}
