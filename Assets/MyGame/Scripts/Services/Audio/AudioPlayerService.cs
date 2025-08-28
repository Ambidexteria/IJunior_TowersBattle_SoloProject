using System.Collections.Generic;
using UnityEngine;

namespace Base.Services.Audio
{
    public class AudioPlayerService : IService
    {
        private readonly AudioSource _musicSource;
        private readonly AudioClip _mainMenuMusic;
        private readonly AudioClip _gameSceneMusic;
        private readonly AudioSource _winSoundSource;
        private readonly AudioSource _defeatSoundSource;

        private readonly AudioSource _soldierDeathSound;
        private readonly List<AudioClip> _soldierAnswerSounds;
        private readonly AudioSource _soldierAnswerSource;
        private readonly AudioSource _soldierShootSource;

        public AudioPlayerService(
            AudioSource musicSource, 
            AudioClip mainMenuMusic,
            AudioClip gameSceneMusic, 
            AudioSource soldierDeathSound, 
            List<AudioClip> soldierAnswerSounds, 
            AudioSource soldierAnswerSource, 
            AudioSource winSoundSource, 
            AudioSource defeatSoundSource,
            AudioSource soldierShootSource)
        {
            _musicSource = musicSource;
            _mainMenuMusic = mainMenuMusic;
            _gameSceneMusic = gameSceneMusic;
            _soldierDeathSound = soldierDeathSound;
            _soldierAnswerSounds = soldierAnswerSounds;
            _soldierAnswerSource = soldierAnswerSource;
            _winSoundSource = winSoundSource;
            _defeatSoundSource = defeatSoundSource;
            _soldierShootSource = soldierShootSource;
        }

        public void PlayMainMenuMusic()
        {
            if (_musicSource.isPlaying)
                _musicSource.Pause();

            _musicSource.clip = _mainMenuMusic;
            _musicSource.Play();
        }

        public void PlayGameSceneMusic()
        {
            if (_musicSource.isPlaying)
                _musicSource.Pause();

            _musicSource.clip = _gameSceneMusic;
            _musicSource.Play();
        }

        public void PlaySoldierDeathSound()
        {
            if (_soldierDeathSound.isPlaying)
                return;

            _soldierDeathSound.Play();
        }

        public void PlaySoldierRandomAnswerSound()
        {
            AudioClip soldierAnswer = _soldierAnswerSounds[Random.Range(0, _soldierAnswerSounds.Count)];
            _soldierAnswerSource.clip = soldierAnswer;
            _soldierAnswerSource.Play();
        }

        public void PlaySoldierShootSound()
        {
            if (_soldierShootSource.isPlaying)
                return;

            _soldierShootSource.Play();
        }

        public void PlayWinSound()
        {
            _winSoundSource.Play();
        }

        public void PlayDefeatSound()
        {
            _defeatSoundSource.Play();
        }
    }
}
