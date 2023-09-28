// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.Assertions;

namespace TriviaQuizKit
{
    /// <summary>
    /// Wrapper around Unity's AudioSource that disables the game object after the sound clip
    /// has been played (allowing it to be reused in the context of a pool of sound effects; see
    /// the SoundManager class).
    /// </summary>
    public class SoundFx : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            Assert.IsTrue(audioSource != null);
        }

        public void Play(AudioClip clip, bool loop = false)
        {
            if (clip == null)
            {
                return;
            }
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
            Invoke(nameof(DisableSoundFx), clip.length + 0.1f);
        }

        private void DisableSoundFx()
        {
            GetComponent<PooledObject>().Pool.ReturnObject(gameObject);
        }
    }
}
