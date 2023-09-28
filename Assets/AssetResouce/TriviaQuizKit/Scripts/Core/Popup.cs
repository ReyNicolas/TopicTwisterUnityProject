// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TriviaQuizKit
{
	/// <summary>
	/// The base class used for all the popups in the game.
	/// </summary>
	public class Popup : MonoBehaviour
	{
		[HideInInspector]
		public BaseScreen ParentScreen;

		public UnityEvent OnOpen;
		public UnityEvent OnClose;

		private Animator animator;

		protected virtual void Awake()
		{
			animator = GetComponent<Animator>();
		}

		protected virtual void Start()
		{
			OnOpen.Invoke();
		}

		protected void Close()
		{
			OnClose.Invoke();
			if (ParentScreen != null)
			{
				ParentScreen.ClosePopup();
			}
			if (animator != null)
			{
				animator.Play("Close");
				StartCoroutine(DestroyPopup());
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private IEnumerator DestroyPopup()
		{
			yield return new WaitForSeconds(0.5f);
			Destroy(gameObject);
		}
	}
}
