// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// The asset type used to store a pack of questions.
	/// </summary>
	[CreateAssetMenu(fileName = "QuestionPack", menuName = "Trivia Quiz Kit/Question pack", order = 2)]
	public class QuestionPack : ScriptableObject
	{
		public string Name;
		public List<BaseQuestion> Questions = new List<BaseQuestion>();
	}
}
