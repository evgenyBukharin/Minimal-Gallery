using UnityEngine;
using Zenject;

public class LoadingServiceInstaller : MonoInstaller
{
    [SerializeField] private LoadingService loadingService;

    public override void InstallBindings()
    {
        Container
            .Bind<LoadingService>()
            .FromInstance(loadingService)
            .AsSingle();
    }
}
