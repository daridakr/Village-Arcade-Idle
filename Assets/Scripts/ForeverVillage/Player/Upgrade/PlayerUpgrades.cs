using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class PlayerUpgrades : MonoBehaviour, IPlayerUpgrades
    {
        public bool CanUpgrade(IPlayerUpgrade upgrade)
        {
            throw new System.NotImplementedException();
        }

        public IPlayerUpgrade[] GetAllUpgrades()
        {
            throw new System.NotImplementedException();
        }

        public IPlayerUpgrade GetUpgrade(string guid)
        {
            throw new System.NotImplementedException();
        }

        public void Upgrade(IPlayerUpgrade upgrade)
        {
            throw new System.NotImplementedException();
        }
    }
}