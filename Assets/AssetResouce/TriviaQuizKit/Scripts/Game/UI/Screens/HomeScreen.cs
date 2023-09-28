// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// The home screen.
	/// </summary>
	public class HomeScreen : BaseScreen
	{
		public List<Sprite> AvatarSprites;

		[SerializeField]
		protected Image AvatarImage;

		private void Start()
		{
			var selectedAvatar = PlayerPrefs.GetInt("player_avatar");
			SetAvatar(selectedAvatar);
		}

		public void OnAvatarButtonPressed()
		{
			OpenPopup<ProfilePopup>("Popups/ProfilePopup");
		}

		public void OnSettingsButtonPressed()
		{
			OpenPopup<SettingsPopup>("Popups/SettingsPopup", popup => {});
		}

		public void SetAvatar(int avatar)
		{
			AvatarImage.sprite = AvatarSprites[avatar];
		}
	}
}
