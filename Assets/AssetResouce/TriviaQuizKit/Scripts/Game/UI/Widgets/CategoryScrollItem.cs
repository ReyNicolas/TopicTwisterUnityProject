// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// A single item in the category scroll displayed in the profile popup.
	/// </summary>
	public class CategoryScrollItem : MonoBehaviour
	{
		public List<Sprite> CupSprites;
		public Image Image;
		public Image CupImage;
		public TextMeshProUGUI NameText;
		public TextMeshProUGUI HighScoreText;
		public Color HighScoreTextColor;
		public Color NoHighScoreTextColor;
	}
}
