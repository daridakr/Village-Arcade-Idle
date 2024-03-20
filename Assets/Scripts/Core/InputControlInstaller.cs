using UnityEngine;
using Zenject;

public class InputControlInstaller : MonoInstaller
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private JoystickInputControl _inputControl;

    public override void InstallBindings()
    {
        BindJoystick();
    }

    private void BindJoystick()
    {
        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        Container.BindInterfacesTo<JoystickInputControl>().FromInstance(_inputControl).AsSingle();
    }
}