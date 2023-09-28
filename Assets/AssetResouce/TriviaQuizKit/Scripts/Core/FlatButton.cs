// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TriviaQuizKit
{
	/// <summary>
	/// Button class used in the kit. It provides a subtle 'pressed' animation effect when clicked/touched.
	/// </summary>
	public class FlatButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		public float PressedDisplacement = 5f;

		public TextMeshProUGUI Text;

		public UnityEvent OnPressedEvent;

		private bool isMouseInside;

		public void OnPointerEnter(PointerEventData pointerEventData)
		{
			isMouseInside = true;
		}

		public void OnPointerExit(PointerEventData pointerEventData)
		{
			isMouseInside = false;
		}

		public void OnPointerDown(PointerEventData pointerEventData)
		{
			var rectTransform = GetComponent<RectTransform>();
			var newPos = rectTransform.anchoredPosition;
			newPos.y -= PressedDisplacement;
			rectTransform.anchoredPosition = newPos;
		}

		public void OnPointerUp(PointerEventData pointerEventData)
		{
			var rectTransform = GetComponent<RectTransform>();
			var newPos = rectTransform.anchoredPosition;
			newPos.y += PressedDisplacement;
			rectTransform.anchoredPosition = newPos;

			if (isMouseInside)
			{
				OnPressedEvent.Invoke();
			}
		}
	}
}
