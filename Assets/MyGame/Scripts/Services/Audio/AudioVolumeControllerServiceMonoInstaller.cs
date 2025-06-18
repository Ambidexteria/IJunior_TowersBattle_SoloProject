using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Base.Services.Audio
{
    public class AudioVolumeControllerServiceMonoInstaller : MonoInstaller
    {
        [SerializeField] private AudioMixerGroup _mixerGroup;

        public override void InstallBindings()
        {
            ExceptionsTest.NullRefMethodTest(nameof(AudioVolumeControllerServiceMonoInstaller), nameof(InstallBindings), _mixerGroup);

            Container.Bind<IAudioVolumeControllerService>().To<AudioVolumeControllerService>().AsSingle().WithArguments(_mixerGroup);
        }
    }
}