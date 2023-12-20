using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class ResidentialViewCreator
    {
        readonly ResidentialViewFactory _factory;

        public ResidentialViewCreator(ResidentialViewFactory factory)
        {
            _factory = factory;
        }

        public void Create(ResidentialView prefab, ResidentialBuilding residential)
        {
            _factory.Create(prefab, residential);
        }
    }
}