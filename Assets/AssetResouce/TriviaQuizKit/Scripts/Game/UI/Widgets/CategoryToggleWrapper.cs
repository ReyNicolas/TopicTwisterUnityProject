// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// A wrapper used for the category toggles in the category selection screen. It allows the individual
	/// toggles to be pressed while also scrolling the containing list.
	/// </summary>
	public class CategoryToggleWrapper : MonoBehaviour
	{
		public CategoryToggle CategoryToggle;
		public FlatButton FlatButton;
		public ToggleButton ToggleButton;
	}
}
