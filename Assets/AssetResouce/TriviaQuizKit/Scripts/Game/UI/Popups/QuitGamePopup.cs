// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine.SceneManagement;

namespace TriviaQuizKit
{
	/// <summary>
	/// The popup that appears when the player wants to exit a game.
	/// </summary>
	public class QuitGamePopup : Popup
	{
		public void OnYesButtonPressed()
		{
			SceneManager.LoadScene("Home");
		}

		public void OnNoButtonPressed()
		{
			Close();
		}
	}
}
