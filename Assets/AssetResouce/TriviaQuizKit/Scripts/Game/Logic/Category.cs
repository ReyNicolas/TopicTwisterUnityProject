// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// This class represents a category. All questions have one or more categories associated to them.
	/// </summary>
	[Serializable]
	public class Category : ScriptableObject
	{
		public string Name;
		public Sprite Sprite;
	}
}
