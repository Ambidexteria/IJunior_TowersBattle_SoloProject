using UnityEngine;
using Zenject;

public class ControlPointDatabaseInstaller : MonoInstaller
{
    [SerializeField] private ControlPointDatabase _controlPointDatabase;

    public override void InstallBindings()
    {
        Container.Bind<ControlPointDatabase>().FromComponentInNewPrefab(_controlPointDatabase).AsSingle().NonLazy();
    }
}