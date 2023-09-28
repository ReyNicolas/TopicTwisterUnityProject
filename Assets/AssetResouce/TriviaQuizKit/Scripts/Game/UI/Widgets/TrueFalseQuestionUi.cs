// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace TriviaQuizKit
{
	/// <summary>
	/// The user interface to display with true or false questions.
	/// </summary>
    public class TrueFalseQuestionUi : QuestionUi
    {
        public TextMeshProUGUI QuestionText;
	    public Image QuestionImage;
        public List<FlatButton> AnswerButtons;
        public Sprite DefaultAnswerSprite;
        public Sprite CorrectAnswerSprite;
        public Sprite WrongAnswerSprite;

	    private int correctAnswerIndex;

        public override void OnQuestionLoaded(GameScreen gameScreen, BaseQuestion question)
        {
		    var trueFalseQuestion = question as TrueFalseQuestion;
		    Assert.IsNotNull(trueFalseQuestion);
		    QuestionText.text = trueFalseQuestion.Question;
		    if (trueFalseQuestion.Image != null)
		    {
			    QuestionImage.sprite = trueFalseQuestion.Image;
			    QuestionImage.preserveAspect = true;
		    }

	        correctAnswerIndex = trueFalseQuestion.IsTrue ? 0 : 1;

	        AnswerButtons[0].OnPressedEvent.RemoveAllListeners();
	        AnswerButtons[1].OnPressedEvent.RemoveAllListeners();
			AnswerButtons[0].OnPressedEvent.AddListener(() => { gameScreen.OnPlayerAnswered(0); });
			AnswerButtons[1].OnPressedEvent.AddListener(() => { gameScreen.OnPlayerAnswered(1); });
        }

        public override bool OnQuestionAnswered(int answerIdx)
        {
			var correct = answerIdx == correctAnswerIndex;
			if (correct)
			{
				HighlightCorrectAnswer();
			}
			else
			{
				HighlightCorrectAnswer();
				HighlightWrongAnswer(answerIdx);
			}

	        AnswerButtons[0].OnPressedEvent.RemoveAllListeners();
	        AnswerButtons[1].OnPressedEvent.RemoveAllListeners();

		    return correct;
        }

        public override void HighlightCorrectAnswer()
        {
			AnswerButtons[correctAnswerIndex].GetComponent<Image>().sprite = CorrectAnswerSprite;
        }

        public override void HighlightWrongAnswer(int answerIdx)
        {
			AnswerButtons[answerIdx].GetComponent<Image>().sprite = WrongAnswerSprite;
        }

	    public override void LockUi()
	    {
			foreach (var button in AnswerButtons)
			{
				button.OnPressedEvent.RemoveAllListeners();
			}
	    }

	    public override void UnlockUi()
	    {
			foreach (var button in AnswerButtons)
			{
				button.GetComponent<Image>().sprite = DefaultAnswerSprite;
			}
	    }

	    public override void OnSelectButtonPressed()
	    {
	    }
    }
}
