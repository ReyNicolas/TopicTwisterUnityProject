// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// Button that can be toggled on or off.
	/// </summary>
	public class ToggleButton : MonoBehaviour, IPointerDownHandler
	{
		public ToggleButtonGroup ToggleGroup;
		public Sprite SelectedSprite;
		public Sprite UnselectedSprite;

		private Image image;

		private void Awake()
		{
			image = GetComponent<Image>();
		}

		public void OnPointerDown(PointerEventData pointerEventData)
		{
			ToggleGroup.SetToggle(this);
		}

		public void SetToggled(bool toggled)
		{
			image.sprite = toggled ? SelectedSprite : UnselectedSprite;
		}
	}
}
