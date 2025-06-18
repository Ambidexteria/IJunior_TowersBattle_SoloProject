using Zenject;

namespace Base.Services.TimeManagment
{
    public class TimeControllerInstaller : Installer<TimeControllerInstaller>
    {
        private float _defaultTimeScale = 1f;
        private float _pauseTimeScale = 0f;
        private float _slowMotionTimeScale = 0.2f;

        public override void InstallBindings()
        {
            TimeController time = new (_defaultTimeScale, _pauseTimeScale, _slowMotionTimeScale);

            Container.Bind<TimeController>().FromInstance(time).AsSingle();
        }
    }
}