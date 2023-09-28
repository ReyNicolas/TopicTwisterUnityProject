// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// The asset type used to store the configuration of the game.
	/// </summary>
	[CreateAssetMenu(fileName = "GameConfiguration", menuName = "Trivia Quiz Kit/Game configuration", order = 1)]
	public class GameConfiguration : ScriptableObject
	{
		public List<Category> Categories;
		public Sprite AnyCategorySprite;

		public int NumQuestions;
		public int NumQuestionsNeededForBronzeTrophy;
		public int NumQuestionsNeededForSilverTrophy;
		public int NumQuestionsNeededForGoldTrophy;

		public int QuestionScore;

		public int TimeLimit;

		public GameObject SingleChoiceQuestionUi;
		public GameObject SingleChoiceWithImageQuestionUi;
		public GameObject MultipleChoiceQuestionUi;
		public GameObject MultipleChoiceWithImageQuestionUi;
		public GameObject TrueFalseQuestionUi;
		public GameObject TrueFalseWithImageQuestionUi;

		public QuestionPackSet PreloadedQuestionPacks;
	}
}
