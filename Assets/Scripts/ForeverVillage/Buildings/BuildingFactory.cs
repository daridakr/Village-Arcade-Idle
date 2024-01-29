using UnityEngine;
using Zenject;

namespace Village
{
    public class BuildingFactory : IBuildingFactory
    {
        private DiContainer _diContainer;

        public BuildingFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Building Create(Building prefab, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<Building>(prefab, parent);
        }
    }

    public interface IBuildingFactory
    {
        public Building Create(Building prefab, Transform parent);
    }
}