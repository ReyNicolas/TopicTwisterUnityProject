// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// Initialization utility component.
	/// </summary>
	public class Initialization : MonoBehaviour
	{
		private void Awake()
		{
			if (!PlayerPrefs.HasKey("sound_enabled"))
			{
				PlayerPrefs.SetInt("sound_enabled", 1);
			}
			if (!PlayerPrefs.HasKey("music_enabled"))
			{
				PlayerPrefs.SetInt("music_enabled", 1);
			}
		}
	}
}
