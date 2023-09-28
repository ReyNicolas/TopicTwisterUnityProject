// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// The base class used in the all the question user interfaces.
	/// </summary>
	public abstract class QuestionUi : MonoBehaviour
	{
		public QuestionResultUi ResultUi;

		public abstract void OnQuestionLoaded(GameScreen gameScreen, BaseQuestion question);
		public abstract bool OnQuestionAnswered(int answerIdx);

		public abstract void HighlightCorrectAnswer();
		public abstract void HighlightWrongAnswer(int answerIdx);

		public abstract void LockUi();
		public abstract void UnlockUi();

		public abstract void OnSelectButtonPressed();
	}
}
