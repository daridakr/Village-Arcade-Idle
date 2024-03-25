using ForeverVillage;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CharacterAnimation>().AsSingle();
    }
}