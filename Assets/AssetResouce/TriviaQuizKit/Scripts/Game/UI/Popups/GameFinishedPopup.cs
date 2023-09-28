// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// The popup that appears when a game is finished.
	/// </summary>
	public class GameFinishedPopup : Popup
	{
		public List<Sprite> TrophySprites = new List<Sprite>();

		public Color ZeroScoreColor = Color.red;

		[SerializeField]
		protected Image TrophyImage;

		[SerializeField]
		protected ParticleSystem ConfettiParticles;

		[SerializeField]
		protected ParticleSystem RainParticles;

		[SerializeField]
		protected TextMeshProUGUI CompletedText;

		[SerializeField]
		protected TextMeshProUGUI ScoreText;

		[SerializeField]
		protected TextMeshProUGUI HighScoreText;

		public void OnReplayButtonPressed()
		{
			((GameScreen)ParentScreen).Restart();
			Close();
		}

		public void OnQuitButtonPressed()
		{
			SceneManager.LoadScene("Home");
		}

		public void SetTrophy(int result, int score, int highScore)
		{
			TrophyImage.sprite = TrophySprites[result];
			if (score == 0)
			{
				ScoreText.color = ZeroScoreColor;
			}

			ScoreText.text = score.ToString();
			HighScoreText.text = highScore.ToString();

			if (result > 0)
			{
				ConfettiParticles.Play();
				CompletedText.text = "Completed!";
				SoundManager.Instance.PlaySound("Win");
			}
			else
			{
				RainParticles.Play();
				CompletedText.text = "Failed ...";
				SoundManager.Instance.PlaySound("Lose");
			}
		}
	}
}
