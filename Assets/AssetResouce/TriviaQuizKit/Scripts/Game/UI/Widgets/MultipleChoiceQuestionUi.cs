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
	/// The user interface to display with multiple choice questions.
	/// </summary>
	public class MultipleChoiceQuestionUi : QuestionUi
	{
        public TextMeshProUGUI QuestionText;
	    public Image QuestionImage;
        public Sprite DefaultAnswerSprite;
        public Sprite SelectedAnswerSprite;
        public Sprite CorrectAnswerSprite;
        public Sprite WrongAnswerSprite;

	    public GameObject AnswerButtonPrefab;
	    public GameObject AnswerButtonsList;

	    private bool firstTime = true;
	    private readonly List<FlatButton> buttons = new List<FlatButton>();

		private readonly List<bool> answers = new List<bool>();
		private readonly List<bool> playerAnswers = new List<bool>();
		private bool playerAnsweredCorrectly;
		private GameScreen gameScreen;
		private bool inputLocked;

	    public override void OnQuestionLoaded(GameScreen screen, BaseQuestion question)
	    {
		    var multipleChoiceQuestion = question as MultipleChoiceQuestion;
		    Assert.IsNotNull(multipleChoiceQuestion);
		    QuestionText.text = multipleChoiceQuestion.Question;
		    if (multipleChoiceQuestion.Image != null)
		    {
			    QuestionImage.sprite = multipleChoiceQuestion.Image;
			    QuestionImage.preserveAspect = true;
		    }

		    gameScreen = screen;

		    var answersCopy = new List<string>(multipleChoiceQuestion.Answers);
		    switch (gameScreen.QuestionOrder)
		    {
			    case GameScreen.QuestionOrderType.Randomized:
					answersCopy.Shuffle();
				    break;
		    }

		    answers.Clear();
		    playerAnswers.Clear();
		    foreach (var _ in multipleChoiceQuestion.Answers)
		    {
			    playerAnswers.Add(false);
		    }

		    foreach (var answer in answersCopy)
		    {
			    var idx = multipleChoiceQuestion.Answers.FindIndex(x => x == answer);
			    answers.Add(idx < multipleChoiceQuestion.NumCorrectAnswers);
		    }

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
			    buttons[i].Text.text = answer;
			    buttons[i].OnPressedEvent.RemoveAllListeners();
			    var iCopy = i;
			    buttons[i].OnPressedEvent.AddListener(() =>
			    {
				    buttons[iCopy].GetComponent<Image>().sprite = playerAnswers[iCopy] ? DefaultAnswerSprite : SelectedAnswerSprite;

				    playerAnswers[iCopy] = !playerAnswers[iCopy];
			    });
			    ++i;
		    }
	    }

	    public override bool OnQuestionAnswered(int answerIdx)
	    {
		    for (var i = 0; i < buttons.Count; i++)
		    {
			    var button = buttons[i];
			    if (answers[i])
			    {
				    if (playerAnswers[i] == answers[i])
				    {
					    button.GetComponent<Image>().sprite = CorrectAnswerSprite;
				    }
				    else
				    {
						button.GetComponent<Image>().sprite = WrongAnswerSprite;
				    }
			    }
			    else
			    {
				    if (playerAnswers[i] != answers[i])
				    {
					    button.GetComponent<Image>().sprite = WrongAnswerSprite;
				    }
			    }
		    }

		    foreach (var button in buttons)
		    {
			    button.OnPressedEvent.RemoveAllListeners();
		    }

		    return playerAnsweredCorrectly;
	    }

		public override void HighlightCorrectAnswer()
		{
			for (var i = 0; i < buttons.Count; i++)
			{
				if (answers[i])
				{
					buttons[i].GetComponent<Image>().sprite = CorrectAnswerSprite;
				}
				else
				{
					buttons[i].GetComponent<Image>().sprite = DefaultAnswerSprite;
				}
			}
		}

		public override void HighlightWrongAnswer(int answerIdx)
		{
		}

	    public override void LockUi()
	    {
		    inputLocked = true;
			foreach (var button in buttons)
			{
				button.OnPressedEvent.RemoveAllListeners();
			}
	    }

	    public override void UnlockUi()
	    {
		    inputLocked = false;
			foreach (var button in buttons)
			{
				button.GetComponent<Image>().sprite = DefaultAnswerSprite;
			}
	    }

	    public override void OnSelectButtonPressed()
	    {
			if (inputLocked)
			{
				return;
			}

			for (var i = 0; i < playerAnswers.Count; i++)
			{
				if (playerAnswers[i] != answers[i])
				{
					playerAnsweredCorrectly = false;
					gameScreen.OnPlayerAnswered(0);
					return;
				}
			}

			playerAnsweredCorrectly = true;
			gameScreen.OnPlayerAnswered(0);
	    }
	}
}
