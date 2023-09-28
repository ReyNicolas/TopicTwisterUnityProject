// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.UI;

namespace TriviaQuizKit
{
    /// <summary>
    /// Utility class for swapping the sprite of a UI Image between two predefined values.
    /// </summary>
    public class SpriteSwapper : MonoBehaviour
    {
        [SerializeField]
        private Sprite enabledSprite = null;

        [SerializeField]
        private Sprite disabledSprite = null;

        private Image image;

        public void Awake()
        {
            image = GetComponent<Image>();
        }

        public void SwapSprite()
        {
            image.sprite = image.sprite == enabledSprite ? disabledSprite : enabledSprite;
        }

        public void SetEnabled(bool spriteEnabled)
        {
            image.sprite = spriteEnabled ? enabledSprite : disabledSprite;
        }
    }
}
