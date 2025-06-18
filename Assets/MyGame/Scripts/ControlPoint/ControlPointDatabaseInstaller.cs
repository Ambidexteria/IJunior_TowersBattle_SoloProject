using UnityEngine;
using Zenject;

public class ControlPointDatabaseInstaller : MonoInstaller
{
    [SerializeField] private ControlPointDatabase _controlPointDatabase;

    public override void InstallBindings()
    {
        ExceptionsTest.NullRefMethodTest(nameof(ControlPointDatabaseInstaller), nameof(InstallBindings), _controlPointDatabase);

        Container.Bind<ControlPointDatabase>().FromComponentInNewPrefab(_controlPointDatabase).AsSingle().NonLazy();
    }
}