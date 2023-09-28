// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// The user interface to display whether the player has answered a question correctly or not.
	/// </summary>
	public class QuestionResultUi : MonoBehaviour
	{
		public Image Image;
	    public TextMeshProUGUI CorrectText;
	    public TextMeshProUGUI WrongText;

		private void OnDisable()
		{
			Hide();
		}

		private void Hide()
		{
			var newColor = Image.color;
			newColor.a = 0;
			Image.color = newColor;

			newColor = CorrectText.color;
			newColor.a = 0;
			CorrectText.color = newColor;

			newColor = WrongText.color;
			newColor.a = 0;
			WrongText.color = newColor;
		}

		public IEnumerator FadeIn(bool correct, float time)
		{
			StartCoroutine(FadeInImage(correct, time));
			StartCoroutine(FadeInText(correct, time));
			yield return null;
		}

		public IEnumerator FadeOut(bool correct, float time)
		{
			StartCoroutine(FadeOutImage(time));
			StartCoroutine(FadeOutText(correct, time));
			yield return null;
		}

        private IEnumerator FadeInImage(bool correct, float time)
        {
            CorrectText.gameObject.SetActive(correct);
            WrongText.gameObject.SetActive(!correct);
            var alpha = Image.color.a;
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
            {
                var newColor = Image.color;
                newColor.a = Mathf.Lerp(alpha, 220 / 256.0f, t);
                Image.color = newColor;
                yield return null;
            }
        }

        private IEnumerator FadeOutImage(float time)
        {
            var alpha = Image.color.a;
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
            {
                var newColor = Image.color;
                newColor.a = Mathf.Lerp(alpha, 0, t);
                Image.color = newColor;
                yield return null;
            }

            var lastColor = Image.color;
            lastColor.a = 0.0f;
            Image.color = lastColor;
        }

        private IEnumerator FadeInText(bool correct, float time)
        {
	        var text = correct ? CorrectText : WrongText;
            var alpha = Image.color.a;
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
            {
                var newColor = text.color;
                newColor.a = Mathf.Lerp(alpha, 1.0f, t);
                text.color = newColor;
                yield return null;
            }
        }

        private IEnumerator FadeOutText(bool correct, float time)
        {
	        var text = correct ? CorrectText : WrongText;
            var alpha = text.color.a;
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
            {
                var newColor = text.color;
                newColor.a = Mathf.Lerp(alpha, 0, t);
                text.color = newColor;
                yield return null;
            }

            var lastColor = text.color;
            lastColor.a = 0.0f;
            text.color = lastColor;
        }
	}
}
