using UnityEngine;
using Zenject;

public class InputSeviceInstaller : MonoInstaller
{
    [SerializeField] private InputService _inputService;
    
    public override void InstallBindings()
    {
        Container
            .Bind<InputService>()
            .FromInstance(_inputService)
            .AsSingle();
    }
}
