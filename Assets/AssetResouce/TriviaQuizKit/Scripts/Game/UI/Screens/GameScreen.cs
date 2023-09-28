// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TriviaQuizKit
{
	/// <summary>
	/// The game screen.
	/// </summary>
	public class GameScreen : BaseScreen
	{
		public enum QuestionOrderType
		{
			Randomized,
			Test
		}

		public QuestionOrderType QuestionOrder;

		public enum QuestionPackLoadType
		{
			All,
			Single
		}

		public QuestionPackLoadType QuestionPackLoad;
		public string QuestionPackToLoadName;

		public TextMeshProUGUI NumCorrectAnswersText;
		public TextMeshProUGUI NumAnsweredText;
		public TextMeshProUGUI ScoreText;
		public TextMeshProUGUI NumRemainingQuestionsText;
		public TextMeshProUGUI CategoryText;
		public TextMeshProUGUI CountdownText;
		public Image CountdownBgImage;
		public Image CountdownImage;
		public Image NoTimeLimitImage;
		public FlatButton SelectButton;

		private readonly List<BaseQuestion> availableQuestions = new List<BaseQuestion>();
		private readonly List<BaseQuestion> usedQuestions = new List<BaseQuestion>();

		private QuestionUi questionUi;

		private QuestionUi singleChoiceUi;
		private QuestionUi singleChoiceWithImageUi;
		private QuestionUi multipleChoiceUi;
		private QuestionUi multipleChoiceWithImageUi;
		private QuestionUi trueFalseUi;
		private QuestionUi trueFalseWithImageUi;
		private readonly List<QuestionUi> availableQuestionUis = new List<QuestionUi>();

		private int numCorrectAnswers;
		private int numWrongAnswers;
		private int numAnswered;

		private int correctAnswerIndex;

		private TimeMode timeMode;
		private bool countdownRunning;
		private float accTime;
		private float timeRemaining;

		private GameConfiguration gameConfig;
		private List<BaseQuestion> questions = new List<BaseQuestion>();

		private bool correct;

		private int currentScore;

		private void Start()
		{
			gameConfig = GameConfigurationLoader.LoadGameConfiguration("GameConfiguration");

			var selectedQuestionType = (QuestionType)PlayerPrefs.GetInt("question_type");
			switch (selectedQuestionType)
			{
				case QuestionType.SingleChoice:
					LoadSingleChoiceQuestionUi();
					break;

				case QuestionType.MultipleChoice:
					LoadMultipleChoiceQuestionUi();
					break;

				case QuestionType.TrueFalse:
					LoadTrueFalseQuestionUi();
					break;

				case QuestionType.Any:
					LoadSingleChoiceQuestionUi();
					LoadMultipleChoiceQuestionUi();
					LoadTrueFalseQuestionUi();
					break;
			}

			LoadQuestions();
			StartGame();
		}

		private void LoadSingleChoiceQuestionUi()
		{
			singleChoiceUi = Instantiate(gameConfig.SingleChoiceQuestionUi).GetComponent<SingleChoiceQuestionUi>();
			singleChoiceUi.transform.SetParent(Canvas.transform, false);
			singleChoiceWithImageUi =
				Instantiate(gameConfig.SingleChoiceWithImageQuestionUi).GetComponent<SingleChoiceQuestionUi>();
			singleChoiceWithImageUi.transform.SetParent(Canvas.transform, false);
			availableQuestionUis.Add(singleChoiceUi);
			availableQuestionUis.Add(singleChoiceWithImageUi);
		}

		private void LoadMultipleChoiceQuestionUi()
		{
			multipleChoiceUi = Instantiate(gameConfig.MultipleChoiceQuestionUi).GetComponent<MultipleChoiceQuestionUi>();
			multipleChoiceUi.transform.SetParent(Canvas.transform, false);
			multipleChoiceWithImageUi = Instantiate(gameConfig.MultipleChoiceWithImageQuestionUi)
				.GetComponent<MultipleChoiceQuestionUi>();
			multipleChoiceWithImageUi.transform.SetParent(Canvas.transform, false);
			availableQuestionUis.Add(multipleChoiceUi);
			availableQuestionUis.Add(multipleChoiceWithImageUi);
		}

		private void LoadTrueFalseQuestionUi()
		{
			trueFalseUi = Instantiate(gameConfig.TrueFalseQuestionUi).GetComponent<TrueFalseQuestionUi>();
			trueFalseUi.transform.SetParent(Canvas.transform, false);
			trueFalseWithImageUi = Instantiate(gameConfig.TrueFalseWithImageQuestionUi).GetComponent<TrueFalseQuestionUi>();
			trueFalseWithImageUi.transform.SetParent(Canvas.transform, false);
			availableQuestionUis.Add(trueFalseUi);
			availableQuestionUis.Add(trueFalseWithImageUi);
		}

		private void Update()
		{
			if (!countdownRunning)
			{
				return;

			}

			accTime += Time.deltaTime;
			timeRemaining -= Time.deltaTime;
			CountdownImage.fillAmount = timeRemaining / gameConfig.TimeLimit;
			if (accTime >= 1.0f)
			{
				accTime = 0.0f;
				CountdownText.text = Mathf.CeilToInt(timeRemaining).ToString();
				if (timeRemaining <= 0)
				{
					StopCountdown();

					correct = false;

					++numWrongAnswers;
					questionUi.HighlightCorrectAnswer();

					SoundManager.Instance.PlaySound("Incorrect");

					AdvanceToNextQuestion();
				}
			}
		}

		private void LoadQuestions()
		{
			switch (QuestionPackLoad)
			{
				case QuestionPackLoadType.All:
					questions = QuestionPackLoader.LoadAllQuestions(gameConfig);
					break;

				case QuestionPackLoadType.Single:
					questions = QuestionPackLoader.LoadQuestions(QuestionPackToLoadName);
					break;
			}

			var questionClassTypes = new List<Type>();
			var questionType = (QuestionType)PlayerPrefs.GetInt("question_type");
			switch (questionType)
			{
				case QuestionType.SingleChoice:
					questionClassTypes.Add(typeof(SingleChoiceQuestion));
					break;

				case QuestionType.MultipleChoice:
					questionClassTypes.Add(typeof(MultipleChoiceQuestion));
					break;

				case QuestionType.TrueFalse:
					questionClassTypes.Add(typeof(TrueFalseQuestion));
					break;

				case QuestionType.Any:
					questionClassTypes.Add(typeof(SingleChoiceQuestion));
					questionClassTypes.Add(typeof(MultipleChoiceQuestion));
					questionClassTypes.Add(typeof(TrueFalseQuestion));
					break;
			}

			if (gameConfig != null && questions.Count > 0)
			{
				var categoryIdx = PlayerPrefs.GetInt("category");
				if (categoryIdx >= 0)
				{
					var category = gameConfig.Categories[categoryIdx];
					foreach (var question in questions)
					{
						if (question.Categories.Contains(category) && questionClassTypes.Contains(question.GetType()))
						{
							availableQuestions.Add(question);
						}
					}
				}
				else
				{
					foreach (var question in questions)
					{
						if (questionClassTypes.Contains(question.GetType()))
						{
							availableQuestions.Add(question);
						}
					}
				}
			}
		}

		private void StartGame()
		{
			NumCorrectAnswersText.text = "0";
			NumAnsweredText.text = "0";
			ScoreText.text = "0";
			NumRemainingQuestionsText.text = (gameConfig.NumQuestions - numAnswered).ToString();

			timeMode = (TimeMode)PlayerPrefs.GetInt("time_mode");
			switch (timeMode)
			{
				case TimeMode.Limited:
					countdownRunning = true;
					timeRemaining = gameConfig.TimeLimit;
					NoTimeLimitImage.gameObject.SetActive(false);
					CountdownText.text = timeRemaining.ToString(CultureInfo.InvariantCulture);
					break;

				case TimeMode.Unlimited:
					countdownRunning = false;
					NoTimeLimitImage.gameObject.SetActive(true);
					CountdownText.gameObject.SetActive(false);
					CountdownBgImage.gameObject.SetActive(false);
					CountdownImage.gameObject.SetActive(false);
					break;
			}

			SelectRandomQuestion();
		}

		private void SelectRandomQuestion()
		{
			if (availableQuestions.Count > 0)
			{
				var idx = 0;
				switch (QuestionOrder)
				{
					case QuestionOrderType.Randomized:
						idx = Random.Range(0, availableQuestions.Count);
						break;
				}
				var question = availableQuestions[idx];
				availableQuestions.Remove(question);
				usedQuestions.Add(question);
				LoadQuestion(question);
			}
			else
			{
				availableQuestions.AddRange(usedQuestions);
				usedQuestions.Clear();
				SelectRandomQuestion();
			}
		}

		private void LoadQuestion(BaseQuestion question)
		{
			if (question is SingleChoiceQuestion)
			{
				var singleChoiceQuestion = question as SingleChoiceQuestion;
				if (singleChoiceQuestion.Image != null)
				{
					questionUi = singleChoiceWithImageUi;
				}
				else
				{
					questionUi = singleChoiceUi;
				}

				SelectButton.gameObject.SetActive(false);
			}
			else if (question is MultipleChoiceQuestion)
			{
				var multipleChoiceQuestion = question as MultipleChoiceQuestion;
				if (multipleChoiceQuestion.Image != null)
				{
					questionUi = multipleChoiceWithImageUi;
				}
				else
				{
					questionUi = multipleChoiceUi;
				}

				SelectButton.gameObject.SetActive(true);
			}
			else if (question is TrueFalseQuestion)
			{
				var trueFalseQuestion = question as TrueFalseQuestion;
				if (trueFalseQuestion.Image != null)
				{
					questionUi = trueFalseWithImageUi;
				}
				else
				{
					questionUi = trueFalseUi;
				}

				SelectButton.gameObject.SetActive(false);
			}

			foreach (var ui in availableQuestionUis)
			{
				ui.gameObject.SetActive(ui == questionUi);
			}
			questionUi.OnQuestionLoaded(this, question);

			CategoryText.text = question.Categories[0].Name;
		}

		public void OnPlayerAnswered(int answerIdx)
		{
			StopCountdown();

			correct = questionUi.OnQuestionAnswered(answerIdx);
			if (correct)
			{
				++numCorrectAnswers;
				NumCorrectAnswersText.text = numCorrectAnswers.ToString();

				currentScore += gameConfig.QuestionScore;
				ScoreText.text = currentScore.ToString();

				SoundManager.Instance.PlaySound("Correct");
			}
			else
			{
				++numWrongAnswers;

				SoundManager.Instance.PlaySound("Incorrect");
			}

			AdvanceToNextQuestion();
		}

		private void AdvanceToNextQuestion()
		{
			++numAnswered;

			NumAnsweredText.text = (numCorrectAnswers + numWrongAnswers).ToString();
			NumRemainingQuestionsText.text = (gameConfig.NumQuestions - numAnswered).ToString();

			if (numAnswered < gameConfig.NumQuestions)
			{
				StartCoroutine(SelectRandomQuestionAsync());
			}
			else
			{
				StartCoroutine(OpenGameFinishedPopupAsync());
			}
		}

		private IEnumerator SelectRandomQuestionAsync()
		{
			questionUi.LockUi();

			StartCoroutine(questionUi.ResultUi.FadeIn(correct, 0.2f));

			yield return new WaitForSeconds(2.0f);

			StartCoroutine(questionUi.ResultUi.FadeOut(correct, 0.2f));

			questionUi.UnlockUi();

			if (timeMode == TimeMode.Limited)
			{
				StartCountdown();
			}

			SelectRandomQuestion();
		}

		private IEnumerator OpenGameFinishedPopupAsync()
		{
			questionUi.LockUi();

			StartCoroutine(questionUi.ResultUi.FadeIn(correct, 0.2f));

			yield return new WaitForSeconds(2.0f);

			StartCoroutine(questionUi.ResultUi.FadeOut(correct, 0.2f));

			questionUi.UnlockUi();

			var result = 0;
			if (numCorrectAnswers >= gameConfig.NumQuestionsNeededForGoldTrophy) result = 3;
			else if (numCorrectAnswers >= gameConfig.NumQuestionsNeededForSilverTrophy) result = 2;
			else if (numCorrectAnswers >= gameConfig.NumQuestionsNeededForBronzeTrophy) result = 1;
			var questionType = PlayerPrefs.GetInt("question_type");
			var category = PlayerPrefs.GetInt("category");
			var str = $"trophy_{questionType}_{category}";
			PlayerPrefs.SetInt(str, result);

			str = $"score_{questionType}_{category}";
			var highScore = PlayerPrefs.GetInt(str);
			if (currentScore > highScore)
			{
				PlayerPrefs.SetInt(str, currentScore);
				highScore = currentScore;
			}

			OpenPopup<GameFinishedPopup>("Popups/GameFinishedPopup", popup =>
			{
				popup.SetTrophy(result, currentScore, highScore);
			});
		}

		private void StartCountdown()
		{
			countdownRunning = true;
			timeRemaining = gameConfig.TimeLimit;
			CountdownText.text = timeRemaining.ToString(CultureInfo.InvariantCulture);
		}

		private void StopCountdown()
		{
			countdownRunning = false;
		}

		public void Restart()
		{
			numCorrectAnswers = 0;
			numWrongAnswers = 0;
			numAnswered = 0;

			currentScore = 0;

			availableQuestions.AddRange(usedQuestions);
			usedQuestions.Clear();

			StartGame();
		}

		public void OnQuitButtonPressed()
		{
			OpenPopup<QuitGamePopup>("Popups/QuitGamePopup");
		}

		public void OnSelectButtonPressed()
		{
			questionUi.OnSelectButtonPressed();
		}
	}
}
