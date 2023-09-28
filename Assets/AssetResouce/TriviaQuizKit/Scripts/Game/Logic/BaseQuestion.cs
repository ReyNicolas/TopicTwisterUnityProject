// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// The base class used for questions.
	/// </summary>
	[Serializable]
	public abstract class BaseQuestion : ScriptableObject
	{
		public List<Category> Categories = new List<Category>();
		public string Question = string.Empty;
		public string Metadata = string.Empty;

#if UNITY_EDITOR
		public abstract void Draw();
#endif
	}
}
