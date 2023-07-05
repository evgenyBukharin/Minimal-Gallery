using UnityEngine;
using Zenject;

public class ScreenServiceInstaller : MonoInstaller
{
    [SerializeField] private ScreenService screenService;

    public override void InstallBindings()
    {
        Container
            .Bind<ScreenService>()
            .FromInstance(screenService)
            .AsSingle();
    }
}
