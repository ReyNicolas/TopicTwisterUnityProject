// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TriviaQuizKit
{
    /// <summary>
    /// The base class used for all the screens in the game.
    /// </summary>
	public abstract class BaseScreen : MonoBehaviour
	{
		[SerializeField]
		protected Canvas Canvas;

	    private readonly Stack<GameObject> currentPopups = new Stack<GameObject>();
	    private readonly Stack<GameObject> currentPanels = new Stack<GameObject>();

        public void OpenPopup<T>(string popupName, Action<T> onOpened = null) where T : Popup
        {
            StartCoroutine(OpenPopupAsync(popupName, onOpened));
        }

        public void ClosePopup()
        {
            var topmostPopup = currentPopups.Pop();
            if (topmostPopup == null)
            {
                return;
            }

            var topmostPanel = currentPanels.Pop();
            if (topmostPanel != null)
            {
                StartCoroutine(FadeOut(topmostPanel.GetComponent<Image>(), 0.2f, () => Destroy(topmostPanel)));
            }
        }

	    private IEnumerator OpenPopupAsync<T>(string popupName, Action<T> onOpened) where T : Popup
        {
            var request = Resources.LoadAsync<GameObject>(popupName);
            while (!request.isDone)
            {
                yield return null;
            }

            var panel = new GameObject("Panel");
            var panelImage = panel.AddComponent<Image>();
            var color = Color.black;
            color.a = 0;
            panelImage.color = color;
            var panelTransform = panel.GetComponent<RectTransform>();
            panelTransform.anchorMin = new Vector2(0, 0);
            panelTransform.anchorMax = new Vector2(1, 1);
            panelTransform.pivot = new Vector2(0.5f, 0.5f);
            panel.transform.SetParent(Canvas.transform, false);
            var panelCanvasGroup = panel.AddComponent<CanvasGroup>();
            panelCanvasGroup.blocksRaycasts = true;
            currentPanels.Push(panel);
            StartCoroutine(FadeIn(panel.GetComponent<Image>(), 0.2f));

            var popup = Instantiate(request.asset) as GameObject;
            Assert.IsNotNull((popup));
            popup.transform.SetParent(Canvas.transform, false);
            popup.GetComponent<Popup>().ParentScreen = this;

            onOpened?.Invoke(popup.GetComponent<T>());
            currentPopups.Push(popup);
        }

	    private static IEnumerator FadeIn(Graphic image, float time)
        {
            var alpha = image.color.a;
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
            {
                var color = image.color;
                color.a = Mathf.Lerp(alpha, 220 / 256.0f, t);
                image.color = color;
                yield return null;
            }
        }

	    private static IEnumerator FadeOut(Graphic image, float time, Action onComplete)
        {
            var alpha = image.color.a;
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
            {
                var color = image.color;
                color.a = Mathf.Lerp(alpha, 0, t);
                image.color = color;
                yield return null;
            }

            onComplete?.Invoke();
        }
	}
}
