using Zenject;

public class ZenjectContainerCreator : MonoInstaller
{
    public override void InstallBindings()
    {
        InputInstaller.Install(Container);
    }
}