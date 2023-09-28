// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// The settings popup.
	/// </summary>
	public class SettingsPopup : Popup
	{
		[SerializeField]
		private Slider musicSlider = null;

		[SerializeField]
		private Slider soundSlider = null;

		private int currentMusic;
		private int currentSound;

		protected override void Start()
		{
			base.Start();

			currentMusic = PlayerPrefs.GetInt("music_enabled");
			currentSound = PlayerPrefs.GetInt("sound_enabled");
			musicSlider.value = currentMusic;
			soundSlider.value = currentSound;
		}

		public void OnInfoButtonPressed()
		{
			ParentScreen.OpenPopup<AlertPopup>("Popups/AlertPopup", popup =>
			{
				popup.SetText("Created by gamevanilla. Copyright (C) 2018.");
			});
		}

		public void OnHelpButtonPressed()
		{
			ParentScreen.OpenPopup<AlertPopup>("Popups/AlertPopup", popup =>
			{
				popup.SetText("If you need any help, please contact us.");
			});
		}

		public void OnCloseButtonPressed()
		{
			SoundManager.Instance.SetMusicEnabled(currentMusic == 1);
			SoundManager.Instance.SetSoundEnabled(currentSound == 1);

			Close();
		}

		public void OnMusicSliderValueChanged()
		{
			currentMusic = (int)musicSlider.value;
		}

		public void OnSoundSliderValueChanged()
		{
			currentSound = (int)soundSlider.value;
		}
	}
}
