// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using TMPro;
using UnityEngine;

namespace TriviaQuizKit
{
	public class AlertPopup : Popup
	{
		[SerializeField]
		protected TextMeshProUGUI Text;

		public void OnCloseButtonPressed()
		{
			Close();
		}

		public void SetText(string text)
		{
			Text.text = text;
		}
	}
}
