using Base.Services.Factories.Game;
using Base.Services.Input;
using Base.Services.PersistentProgress;
using Base.Services.SceneManagment;
using Base.Services.TimeManagment;
using Zenject;

public class BaseServicesInstaller : MonoInstaller
{
    private ProjectContext _projectContext;
    private DiContainer _container;

    public override void InstallBindings()
    {
        _projectContext = ProjectContext.Instance;
        _projectContext.EnsureIsInitialized();
        _container = _projectContext.Container;

        SceneLoaderInstaller.Install(_container);
        InputInstaller.Install(_container);
        AssetLoaderInstaller.Install(_container);
        GameFactoryInstaller.Install(_container);
        PersisentProgressServiceInstaller.Install(_container);
        SaveLoadServiceInstaller.Install(_container); 
        TimeControllerInstaller.Install(_container);
        SceneChangerInstaller.Install(_container);

        GameInstaller.Install(_container);
    }
}