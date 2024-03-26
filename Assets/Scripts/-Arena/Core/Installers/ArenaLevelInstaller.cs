using Zenject;

namespace Arena
{
    public sealed class ArenaLevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProgress();
        }

        private void BindProgress()
        {
            Container.BindInterfacesAndSelfTo<ArenaLevelProgress>().AsSingle();
        }
    }
}