using Zenject;

namespace ForeverVillage.Scripts
{
    public class ResidentialViewFactory : IResidentialViewFactory
    {
        private readonly DiContainer _container;

        public ResidentialViewFactory(DiContainer container)
        {
            _container = container;
        }

        public void Create(ResidentialView prefab, ResidentialBuilding residential)
        {
            _container.InstantiatePrefab(prefab, residential.transform);
        }
    }

    public interface IResidentialViewFactory
    {
        public void Create(ResidentialView prefab, ResidentialBuilding residential);
    }
}