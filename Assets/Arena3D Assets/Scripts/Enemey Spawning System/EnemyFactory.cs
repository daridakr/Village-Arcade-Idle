using Zenject;

namespace Vampire
{
    public class EnemyFactory : IEnemyFactory
    {
        private DiContainer _diContainer;

        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public NPC Create(NPC prefab)
        {
            return _diContainer.InstantiatePrefabForComponent<NPC>(prefab);
        }
    }

    public interface IEnemyFactory
    {
        public NPC Create(NPC prefab);
    }
}