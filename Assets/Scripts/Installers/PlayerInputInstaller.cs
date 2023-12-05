using UnityEngine;
using Zenject;

public class PlayerInputInstaller : MonoInstaller
{
    [SerializeField] private JoystickInputControl _playerControl;
    [SerializeField] private Joystick _joystick;

    public override void InstallBindings()
    {
        BindJoystick();
    }

    private void BindJoystick()
    {
        Container.BindInterfacesTo<JoystickInputControl>().
          FromComponentInNewPrefab(_playerControl).AsSingle();

        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
    }
}
