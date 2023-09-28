// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

using Object = UnityEngine.Object;

namespace TriviaQuizKit
{
	/// <summary>
	/// The 'Questions' tab in the visual editor.
	/// </summary>
	public class QuestionsTab : EditorTab
	{
		private Object questionDbObj;
		private QuestionPack questionPack;

		private ReorderableList questionsList;
		private BaseQuestion currentQuestion;

		private ReorderableList categoryList;
		private Category currentQuestionCategory;

		private Vector2 scrollPos;

		private bool isDirty;

		public QuestionsTab(TriviaQuizKitEditor editor) : base(editor)
		{
			var path = EditorPrefs.GetString("QuestionDbPath");
			if (!string.IsNullOrEmpty(path))
			{
				questionDbObj = AssetDatabase.LoadAssetAtPath(path, typeof(QuestionPack));
				if (questionDbObj != null)
				{
					questionPack = (QuestionPack) questionDbObj;
					CreateQuestionList();
				}
			}
		}

		public override void Draw()
		{
			GUILayout.Space(10);

			var oldDb = questionDbObj;
			questionDbObj = EditorGUILayout.ObjectField("Question pack", questionPack, typeof(QuestionPack), false, GUILayout.Width(430));
			if (questionDbObj != oldDb)
			{
				questionPack = (QuestionPack)questionDbObj;
				EditorPrefs.SetString("QuestionDbPath", AssetDatabase.GetAssetPath(questionDbObj));
				CreateQuestionList();
			}

			if (questionPack != null)
			{
				GUILayout.Space(10);

				var oldName = questionPack.Name;
				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(new GUIContent("Name", "The name of this question pack."),
					GUILayout.Width(70));
				questionPack.Name = EditorGUILayout.TextField(questionPack.Name, GUILayout.Width(150));
				GUILayout.EndHorizontal();
				if (questionPack.Name != oldName)
				{
					isDirty = true;
				}

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("Import from .csv (single-choice)", GUILayout.Width(200)))
				{
					ImportQuestionsFromCsv(QuestionType.SingleChoice);
				}
				else if (GUILayout.Button("Import from .csv (multiple-choice)", GUILayout.Width(200)))
				{
					ImportQuestionsFromCsv(QuestionType.MultipleChoice);
				}
				else if (GUILayout.Button("Import from .csv (true-false)", GUILayout.Width(200)))
				{
					ImportQuestionsFromCsv(QuestionType.TrueFalse);
				}
				GUILayout.EndHorizontal();

				GUILayout.Space(10);

				GUILayout.BeginHorizontal();

				scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false, GUILayout.Width(320),
					GUILayout.Height(700));

				GUILayout.BeginVertical(GUILayout.Width(300));
				questionsList?.DoLayoutList();
				GUILayout.EndVertical();

				EditorGUILayout.EndScrollView();

				if (currentQuestion != null)
				{
					DrawQuestion(currentQuestion);
				}

				GUILayout.EndHorizontal();

				if (isDirty)
				{
					isDirty = false;
					EditorUtility.SetDirty(questionPack);
				}
			}
		}

		private void DrawQuestion(BaseQuestion question)
		{
			var originalWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 60;

			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(500));

			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical(GUILayout.Width(250));
			categoryList?.DoLayoutList();
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			GUILayout.Space(10);

			question.Draw();

			GUILayout.EndVertical();

			EditorGUIUtility.labelWidth = originalWidth;

		}

		private void CreateQuestionList()
		{
			questionsList = SetupReorderableList("Questions", questionPack.Questions, ref currentQuestion, (rect, x) =>
				{
					EditorGUI.LabelField(new Rect(rect.x, rect.y, 350, EditorGUIUtility.singleLineHeight), EditorUtils.GetEditorFriendlyText(x.Question, 40));
				},
				x => { currentQuestion = x; CreateCategoriesList(currentQuestion); },
				() =>
				{
					var menu = new GenericMenu();
					menu.AddItem(new GUIContent("Single choice question"), false, CreateQuestionCallback, typeof(SingleChoiceQuestion));
					menu.AddItem(new GUIContent("Multiple choice question"), false, CreateQuestionCallback, typeof(MultipleChoiceQuestion));
					menu.AddItem(new GUIContent("True or false question"), false, CreateQuestionCallback, typeof(TrueFalseQuestion));
					menu.ShowAsContext();
				},
				x =>
				{
					Object.DestroyImmediate(currentQuestion, true);
					currentQuestion = null;
					EditorUtility.SetDirty(ParentEditor.GameConfig);
				});
		}

		private void CreateQuestionCallback(object obj)
		{
			var question = ScriptableObject.CreateInstance((Type)obj) as BaseQuestion;
			questionPack.Questions.Add(question);
			AssetDatabase.AddObjectToAsset(question, questionPack);
			EditorUtility.SetDirty(questionPack);
		}

		private void CreateCategoriesList(BaseQuestion question)
		{
			categoryList = SetupReorderableList("Categories", question.Categories, ref currentQuestionCategory, (rect, x) =>
				{
					if (x != null)
					{
						EditorGUI.LabelField(new Rect(rect.x, rect.y, 350, EditorGUIUtility.singleLineHeight), x.Name);
					}
				},
				x => { currentQuestionCategory = x; },
				() =>
				{
					var menu = new GenericMenu();
					foreach (var category in ParentEditor.GameConfig.Categories)
					{
						menu.AddItem(new GUIContent(category.Name), false, CreateQuestionCategoryCallback, category);
					}
					menu.ShowAsContext();
				},
				x => { currentQuestionCategory = null; });
		}

		private void CreateQuestionCategoryCallback(object obj)
		{
			currentQuestion.Categories.Add((Category)obj);
			EditorUtility.SetDirty(questionPack);
		}

		private void ImportQuestionsFromCsv(QuestionType questionType)
		{
			var path = EditorUtility.OpenFilePanel("Select .csv file", "", "csv");
			if (path.Length != 0)
			{
				using (var streamReader = new StreamReader(path))
					ProcessCsvQuestions(streamReader.ReadToEnd(), questionType);
			}
		}

		private void ProcessCsvQuestions(string csv, QuestionType questionType)
		{
			if (questionType == QuestionType.SingleChoice)
			{
				var lines = csv.Split(Environment.NewLine[0]);
				for (var i = 1; i < lines.Length; ++i)
				{
					var line = lines[i];
					if (string.IsNullOrEmpty(line))
						continue;

					var fields = line.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					if (fields.Length < 5)
						continue;

					var question = ScriptableObject.CreateInstance(typeof(SingleChoiceQuestion)) as BaseQuestion;
					var singleChoiceQuestion = (SingleChoiceQuestion)question;
					if (singleChoiceQuestion == null)
						continue;

					var category = ParentEditor.GameConfig.Categories.Find(x => x.Name == fields[0]);
					if (category != null)
						singleChoiceQuestion.Categories.Add(category);
					singleChoiceQuestion.Question = fields[1];
					singleChoiceQuestion.Metadata = fields[2];
					for (var j = 3; j < fields.Length; ++j)
					{
						if (!string.IsNullOrEmpty(fields[j]))
							singleChoiceQuestion.Answers.Add(fields[j]);
					}

					questionPack.Questions.Add(question);
					AssetDatabase.AddObjectToAsset(question, questionPack);
					EditorUtility.SetDirty(questionPack);
				}
			}
			else if (questionType == QuestionType.MultipleChoice)
			{
				var lines = csv.Split(Environment.NewLine[0]);
				for (var i = 1; i < lines.Length; ++i)
				{
					var line = lines[i];
					if (string.IsNullOrEmpty(line))
						continue;

					var fields = line.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					if (fields.Length < 5)
						continue;

					var question = ScriptableObject.CreateInstance(typeof(MultipleChoiceQuestion)) as BaseQuestion;
					var multipleChoiceQuestion = (MultipleChoiceQuestion)question;
					if (multipleChoiceQuestion == null)
						continue;

					var category = ParentEditor.GameConfig.Categories.Find(x => x.Name == fields[0]);
					if (category != null)
						multipleChoiceQuestion.Categories.Add(category);
					multipleChoiceQuestion.Question = fields[1];
					multipleChoiceQuestion.Metadata = fields[2];
					multipleChoiceQuestion.NumCorrectAnswers = int.Parse(fields[3]);
					for (var j = 4; j < fields.Length; ++j)
					{
						if (!string.IsNullOrEmpty(fields[j]))
							multipleChoiceQuestion.Answers.Add(fields[j]);
					}

					questionPack.Questions.Add(question);
					AssetDatabase.AddObjectToAsset(question, questionPack);
					EditorUtility.SetDirty(questionPack);
				}
			}
			else if (questionType == QuestionType.TrueFalse)
			{
				var lines = csv.Split(Environment.NewLine[0]);
				for (var i = 1; i < lines.Length; ++i)
				{
					var line = lines[i];
					if (string.IsNullOrEmpty(line))
						continue;

					var fields = line.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					if (fields.Length < 4)
						continue;

					var question = ScriptableObject.CreateInstance(typeof(TrueFalseQuestion)) as BaseQuestion;
					var multipleChoiceQuestion = (TrueFalseQuestion)question;
					if (multipleChoiceQuestion == null)
						continue;

					var category = ParentEditor.GameConfig.Categories.Find(x => x.Name == fields[0]);
					if (category != null)
						multipleChoiceQuestion.Categories.Add(category);
					multipleChoiceQuestion.Question = fields[1];
					multipleChoiceQuestion.Metadata = fields[2];
					multipleChoiceQuestion.IsTrue = fields[3] == "TRUE";

					questionPack.Questions.Add(question);
					AssetDatabase.AddObjectToAsset(question, questionPack);
					EditorUtility.SetDirty(questionPack);
				}
			}
		}
	}
}
