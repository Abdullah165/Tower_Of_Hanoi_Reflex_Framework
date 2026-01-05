using Reflex.Core;
using UnityEngine;

public class GameInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(DiskSpawnerService), typeof(IDiskSpawnerService));

        containerBuilder.AddSingleton(typeof(ValidationMovementUIService), typeof(IValidationMovementUIService));

        containerBuilder.AddSingleton(typeof(SettingsService), typeof(ISettingsService));
    }
}
