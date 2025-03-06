using UnityEngine;
using Zenject;

public class TeamColorsDatabaseInstaller : MonoInstaller
{
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Material _npcMaterial;
    [SerializeField] private Material _defaultMaterial;

    public override void InstallBindings()
    {
        Container.Bind<TeamColorDatabase>().AsSingle().WithArguments(_playerMaterial, _npcMaterial, _defaultMaterial).NonLazy();
    }
}