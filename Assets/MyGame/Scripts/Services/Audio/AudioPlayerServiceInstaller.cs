using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.Services.Audio
{
    public class AudioPlayerServiceInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip _mainMenuMusic;
        [SerializeField] private AudioClip _gameSceneMusic;
        [SerializeField] private AudioSource _winSoundSource;
        [SerializeField] private AudioSource _defeatSoundSource;

        [SerializeField] private AudioSource _soldierDeathSound;
        [SerializeField] private List<AudioClip> _soldierAnswerSounds;
        [SerializeField] private AudioSource _soldierAnswerSource;
        [SerializeField] private AudioSource _soldierShootSource;

        public override void InstallBindings()
        {
            AudioPlayerService audioPlayerService = new AudioPlayerService(
                _musicSource, 
                _mainMenuMusic, 
                _gameSceneMusic, 
                _soldierDeathSound, 
                _soldierAnswerSounds, 
                _soldierAnswerSource, 
                _winSoundSource, 
                _defeatSoundSource, 
                _soldierShootSource);

            Container.Bind<AudioPlayerService>().FromInstance(audioPlayerService).AsSingle();
        }
    }
}