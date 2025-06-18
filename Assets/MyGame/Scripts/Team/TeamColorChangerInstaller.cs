using UnityEngine;
using Zenject;

public class TeamColorChangerInstaller : MonoInstaller
{
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private Material _npcMaterial;
    [SerializeField] private Material _defaultMaterial;

    public override void InstallBindings()
    {
        ExceptionsTest.NullRefMethodTest(nameof(TeamColorChangerInstaller), nameof(InstallBindings),
            _playerMaterial, _npcMaterial, _defaultMaterial);

        Container.Bind<TeamColorChanger>().AsSingle().WithArguments(_playerMaterial, _npcMaterial, _defaultMaterial).NonLazy();
    }
}