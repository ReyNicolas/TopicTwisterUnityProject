// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// The user interface to display with single choice questions.
	/// </summary>
    public class SingleChoiceQuestionUi : QuestionUi
    {
        public TextMeshProUGUI QuestionText;
	    public Image QuestionImage;
        public Sprite DefaultAnswerSprite;
        public Sprite CorrectAnswerSprite;
        public Sprite WrongAnswerSprite;

	    public GameObject AnswerButtonPrefab;
	    public GameObject AnswerButtonsList;

	    private int correctAnswerIndex;

	    private bool firstTime = true;
	    private readonly List<FlatButton> buttons = new List<FlatButton>();

	    public override void OnQuestionLoaded(GameScreen gameScreen, BaseQuestion question)
	    {
		    var singleChoiceQuestion = question as SingleChoiceQuestion;
		    Assert.IsNotNull(singleChoiceQuestion);
		    QuestionText.text = singleChoiceQuestion.Question;
		    if (singleChoiceQuestion.Image != null)
		    {
			    QuestionImage.sprite = singleChoiceQuestion.Image;
			    QuestionImage.preserveAspect = true;
		    }

		    var answersCopy = new List<string>(singleChoiceQuestion.Answers);
		    switch (gameScreen.QuestionOrder)
		    {
			    case GameScreen.QuestionOrderType.Randomized:
					answersCopy.Shuffle();
				    break;
		    }

		    correctAnswerIndex = answersCopy.FindIndex(x => x == singleChoiceQuestion.Answers[0]);

		    if (firstTime)
		    {
			    firstTime = false;
			    for (var _ = 0; _ < 4; _++)
			    {
				    var button = Instantiate(AnswerButtonPrefab).GetComponent<FlatButton>();
				    button.transform.SetParent(AnswerButtonsList.transform, false);
				    buttons.Add(button);
			    }
		    }

		    var i = 0;
		    foreach (var answer in answersCopy)
		    {
			    var button = buttons[i];
			    button.Text.text = answer;
			    var iCopy = i;
			    button.OnPressedEvent.RemoveAllListeners();
			    button.OnPressedEvent.AddListener(() => { gameScreen.OnPlayerAnswered(iCopy); });
			    ++i;
		    }
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

		    foreach (var button in buttons)
		    {
			    button.OnPressedEvent.RemoveAllListeners();
		    }

		    return correct;
	    }

	    public override void HighlightCorrectAnswer()
		{
			buttons[correctAnswerIndex].GetComponent<Image>().sprite = CorrectAnswerSprite;
		}

		public override void HighlightWrongAnswer(int answerIdx)
		{
			buttons[answerIdx].GetComponent<Image>().sprite = WrongAnswerSprite;
		}

	    public override void LockUi()
	    {
			foreach (var button in buttons)
			{
				button.OnPressedEvent.RemoveAllListeners();
			}
	    }

	    public override void UnlockUi()
	    {
			foreach (var button in buttons)
			{
				button.GetComponent<Image>().sprite = DefaultAnswerSprite;
			}
	    }

	    public override void OnSelectButtonPressed()
	    {
	    }
    }
}
