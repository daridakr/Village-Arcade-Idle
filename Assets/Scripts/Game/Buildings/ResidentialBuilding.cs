using UnityEngine;

public class ResidentialBuilding : Building
{
    [SerializeField] private int _gems = 4;
    [SerializeField] private int _exp = 1;
    [SerializeField] private int _villagers = 1;
    //private Villager[] _villagers;
    //list of views of this building

    private void Start()
    {
        _upgradeMultiplier = 1;
    }

    public override void Upgrade()
    {
        base.Upgrade();

        _gems += (int)_upgradeMultiplier;
        //CapacityUpdated?.Invoke(_capacity);
    }

    // gemsGiver
    // gemGenerationTimeRate
    // StartGemGeneration()
    // Populate(Villager newVillager)
}
