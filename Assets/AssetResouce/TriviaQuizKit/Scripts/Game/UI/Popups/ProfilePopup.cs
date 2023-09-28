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
	/// The player profile popup.
	/// </summary>
	public class ProfilePopup : Popup
	{
		public List<Sprite> AvatarSprites;

		[SerializeField]
		protected Image AvatarImage;

		[SerializeField]
		protected TextMeshProUGUI QuestionTypeText;

		[SerializeField]
		protected GameObject CategoryScrollContent;

		[SerializeField]
		protected GameObject CategoryScrollItemPrefab;

		private GameConfiguration gameConfig;

		private int selectedAvatar;
		private int selectedQuestionType;

		private List<CategoryScrollItem> categoryItems = new List<CategoryScrollItem>();

		protected override void Start()
		{
			base.Start();

			selectedAvatar = PlayerPrefs.GetInt("player_avatar");
			SetAvatar();
			SetQuestionTypeText();
			CreateCategories();
			LoadCategoryInfo();
		}

		public void OnCloseButtonPressed()
		{
			Close();
		}

		public void OnAvatarButtonPressed()
		{
			++selectedAvatar;
			if (selectedAvatar == AvatarSprites.Count)
			{
				selectedAvatar = 0;
			}
			SetAvatar();
			((HomeScreen)ParentScreen).SetAvatar(selectedAvatar);
		}

		public void OnPrevButtonPressed()
		{
			--selectedQuestionType;
			if (selectedQuestionType < 0)
			{
				selectedQuestionType = 2;
			}
			SetQuestionTypeText();
			LoadCategoryInfo();
		}

		public void OnNextButtonPressed()
		{
			++selectedQuestionType;
			if (selectedQuestionType == 3)
			{
				selectedQuestionType = 0;
			}
			SetQuestionTypeText();
			LoadCategoryInfo();
		}

		public void OnResetProgressButtonPressed()
		{
			var oldMusic = PlayerPrefs.GetInt("music_enabled");
			var oldSound = PlayerPrefs.GetInt("sound_enabled");
			var oldAvatar = PlayerPrefs.GetInt("player_avatar");
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("music_enabled", oldMusic);
			PlayerPrefs.SetInt("sound_enabled", oldSound);
			PlayerPrefs.SetInt("player_avatar", oldAvatar);
			LoadCategoryInfo();
		}

		private void SetAvatar()
		{
			AvatarImage.sprite = AvatarSprites[selectedAvatar];
			PlayerPrefs.SetInt("player_avatar", selectedAvatar);
		}

		private void SetQuestionTypeText()
		{
			switch (selectedQuestionType)
			{
				case 0:
					QuestionTypeText.text = "Single choice";
					break;

				case 1:
					QuestionTypeText.text = "Multiple choice";
					break;

				case 2:
					QuestionTypeText.text = "True/false";
					break;
			}
		}

		private void CreateCategories()
		{
			gameConfig = GameConfigurationLoader.LoadGameConfiguration("GameConfiguration");
			if (gameConfig != null)
			{
				foreach (var category in gameConfig.Categories)
				{
					var categoryItemGo = Instantiate(CategoryScrollItemPrefab);
					categoryItemGo.transform.SetParent(CategoryScrollContent.transform, false);

					var categoryItem = categoryItemGo.GetComponent<CategoryScrollItem>();
					categoryItem.Image.sprite = category.Sprite;
					categoryItem.NameText.text = category.Name;

					categoryItems.Add(categoryItem);
				}
			}
		}

		private void LoadCategoryInfo()
		{
			for (var i = 0; i < gameConfig.Categories.Count; i++)
			{
				var str = $"trophy_{selectedQuestionType}_{i}";
				var pref = PlayerPrefs.GetInt(str);
				categoryItems[i].CupImage.sprite = categoryItems[i].CupSprites[pref];
				str = $"score_{selectedQuestionType}_{i}";
				var highScore = PlayerPrefs.GetInt(str);
				categoryItems[i].HighScoreText.text = highScore.ToString();
				if (highScore == 0)
				{
					categoryItems[i].HighScoreText.color = categoryItems[i].NoHighScoreTextColor;
				}
				else
				{
					categoryItems[i].HighScoreText.color = categoryItems[i].HighScoreTextColor;
				}
			}
		}
	}
}
