// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// Utility class to play a sound via the SoundManager.
    /// </summary>
    public class PlaySound : MonoBehaviour
    {
        public void Play(string soundName)
        {
            SoundManager.Instance.PlaySound(soundName);
        }
    }
}
