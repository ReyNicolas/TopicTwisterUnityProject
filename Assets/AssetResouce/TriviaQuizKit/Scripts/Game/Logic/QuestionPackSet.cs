// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// The asset type used to store a list of question packs.
	/// </summary>
	[CreateAssetMenu(fileName = "QuestionPackSet", menuName = "Trivia Quiz Kit/Question pack set", order = 3)]
	public class QuestionPackSet : ScriptableObject
	{
		public List<QuestionPack> Packs;
	}
}
