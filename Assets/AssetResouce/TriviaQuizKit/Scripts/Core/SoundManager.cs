// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// This class is the entry point to the sound management of the game.
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        public List<AudioClip> Sounds;
        public static SoundManager Instance;

        private ObjectPool soundPool;
        private readonly Dictionary<string, AudioClip> nameToSound = new Dictionary<string, AudioClip>();

        private BackgroundMusic bgMusic;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            soundPool = GetComponent<ObjectPool>();
        }

        private void Start()
        {
            foreach (var sound in Sounds)
            {
                nameToSound.Add(sound.name, sound);
            }
            bgMusic = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>();
        }

        public void AddSounds(List<AudioClip> soundsToAdd)
        {
            foreach (var sound in soundsToAdd)
            {
                nameToSound.Add(sound.name, sound);
            }
        }

        public void RemoveSounds(List<AudioClip> soundsToAdd)
        {
            foreach (var sound in soundsToAdd)
            {
                nameToSound.Remove(sound.name);
            }
        }

        public void PlaySound(AudioClip clip, bool loop = false)
        {
            var sound = PlayerPrefs.GetInt("sound_enabled");
            if (sound != 1)
            {
                return;
            }

            if (clip != null)
            {
                soundPool.GetObject().GetComponent<SoundFx>().Play(clip, loop);
            }
        }

        public void PlaySound(string soundName, bool loop = false)
        {
            var clip = nameToSound[soundName];
            if (clip != null)
            {
                PlaySound(clip, loop);
            }
        }

        public void SetSoundEnabled(bool soundEnabled)
        {
            PlayerPrefs.SetInt("sound_enabled", soundEnabled ? 1 : 0);
        }

        public void SetMusicEnabled(bool musicEnabled)
        {
            PlayerPrefs.SetInt("music_enabled", musicEnabled ? 1 : 0);
            bgMusic.GetComponent<AudioSource>().mute = !musicEnabled;
        }

        public void ToggleSound()
        {
            var sound = PlayerPrefs.GetInt("sound_enabled");
            PlayerPrefs.SetInt("sound_enabled", 1 - sound);
        }

        public void ToggleMusic()
        {
            var music = PlayerPrefs.GetInt("music_enabled");
            PlayerPrefs.SetInt("music_enabled", 1 - music);
            bgMusic.GetComponent<AudioSource>().mute = (1 - music) == 0;
        }
    }
}
