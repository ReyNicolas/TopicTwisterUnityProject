// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// This class manages the background music of the game.
    /// </summary>
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic instance;

        private AudioSource audioSource;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            var music = PlayerPrefs.GetInt("music_enabled");
            audioSource.mute = music == 0;
            audioSource.Play();
        }
    }
}
