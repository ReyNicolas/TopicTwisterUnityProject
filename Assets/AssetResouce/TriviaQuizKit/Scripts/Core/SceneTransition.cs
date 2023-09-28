// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TriviaQuizKit
{
	/// <summary>
	/// Utility component to perform scene transitions.
	/// </summary>
	public class SceneTransition : MonoBehaviour
	{
		public void TravelToScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}
