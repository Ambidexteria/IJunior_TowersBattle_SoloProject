using Zenject;

namespace Base.Services.Localization
{
    public class LocalizationServiceInstaller : Installer<LocalizationServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocalizationService>().To<LeanLocalizationService>().AsSingle();
        }
    }
}
