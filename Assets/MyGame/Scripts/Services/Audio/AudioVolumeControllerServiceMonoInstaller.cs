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
            Container.Bind<IAudioVolumeControllerService>().To<AudioVolumeControllerService>().AsSingle().WithArguments(_mixerGroup);
        }
    }
}