// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// The 'Game configuration' tab in the visual editor.
	/// </summary>
	public class GameConfigurationTab : EditorTab
	{
		private Object gameConfigurationDbObj;
		private ReorderableList categoryList;
		private Category currentCategory;
		private bool isDirty;

		public GameConfigurationTab(TriviaQuizKitEditor editor) : base(editor)
		{
			var path = EditorPrefs.GetString("GameConfigurationPath");
			if (!string.IsNullOrEmpty(path))
			{
				gameConfigurationDbObj = AssetDatabase.LoadAssetAtPath(path, typeof(GameConfiguration));
				if (gameConfigurationDbObj != null)
				{
					ParentEditor.GameConfig = (GameConfiguration) gameConfigurationDbObj;
					CreateCategoryList();
				}
			}
		}

		public override void Draw()
		{
			var originalWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 50;

			GUILayout.Space(10);

			var oldDb = gameConfigurationDbObj;
			gameConfigurationDbObj = EditorGUILayout.ObjectField("Asset", gameConfigurationDbObj, typeof(GameConfiguration), false, GUILayout.Width(340));
			if (gameConfigurationDbObj != oldDb)
			{
				isDirty = true;
				ParentEditor.GameConfig = (GameConfiguration)gameConfigurationDbObj;
				EditorPrefs.SetString("GameConfigurationPath", AssetDatabase.GetAssetPath(gameConfigurationDbObj));
				CreateCategoryList();
			}

			if (ParentEditor.GameConfig != null)
			{
				GUILayout.Space(10);
				DrawCategoriesPanel();
				DrawGamePanel();
				DrawGameUiPanel();

				EditorGUIUtility.labelWidth = originalWidth;

				if (isDirty)
				{
					isDirty = false;
					EditorUtility.SetDirty(ParentEditor.GameConfig);
				}
			}
		}

		private void DrawCategoriesPanel()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(500));

			var style = new GUIStyle
			{
				fontSize = 20,
				fontStyle = FontStyle.Bold,
				normal = { textColor = Color.white }
			};
			EditorGUILayout.LabelField("Categories", style);
			GUILayout.Space(20);

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical(GUILayout.Width(250));
			categoryList?.DoLayoutList();
			GUILayout.EndVertical();

			if (currentCategory != null)
			{
				DrawCategory(currentCategory);
			}

			GUILayout.EndHorizontal();

			GUILayout.Space(10);

			var oldSprite = ParentEditor.GameConfig.AnyCategorySprite;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("\"Any\" category sprite", "The sprite representing the \"Any\" category."), GUILayout.Width(130));
			ParentEditor.GameConfig.AnyCategorySprite = (Sprite)EditorGUILayout.ObjectField("", ParentEditor.GameConfig.AnyCategorySprite, typeof(Sprite), false, GUILayout.Width(70));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.AnyCategorySprite != oldSprite)
			{
				isDirty = true;
			}

			GUILayout.EndVertical();
		}

		private void DrawGamePanel()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(500));

			var style = new GUIStyle
			{
				fontSize = 20,
				fontStyle = FontStyle.Bold,
				normal = { textColor = Color.white }
			};
			EditorGUILayout.LabelField("Game", style);
			GUILayout.Space(20);

			var oldQuestionPacks = ParentEditor.GameConfig.PreloadedQuestionPacks;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Question packs", "The question packs loaded by default."), GUILayout.Width(130));
			ParentEditor.GameConfig.PreloadedQuestionPacks = (QuestionPackSet)EditorGUILayout.ObjectField(ParentEditor.GameConfig.PreloadedQuestionPacks, typeof(QuestionPackSet), false, GUILayout.Width(300));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.PreloadedQuestionPacks != oldQuestionPacks)
			{
				isDirty = true;
			}

			var oldNumQuestions = ParentEditor.GameConfig.NumQuestions;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Number of questions", "The number of questions played during a game."), GUILayout.Width(130));
			ParentEditor.GameConfig.NumQuestions = EditorGUILayout.IntField(ParentEditor.GameConfig.NumQuestions, GUILayout.Width(30));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.NumQuestions != oldNumQuestions)
			{
				isDirty = true;
			}

			var oldTimeLimit = ParentEditor.GameConfig.TimeLimit;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Time limit", "The time (in seconds) for answering a question in timed mode."), GUILayout.Width(130));
			ParentEditor.GameConfig.TimeLimit = EditorGUILayout.IntField(ParentEditor.GameConfig.TimeLimit, GUILayout.Width(30));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.TimeLimit != oldTimeLimit)
			{
				isDirty = true;
			}

			var oldQuestionScore = ParentEditor.GameConfig.QuestionScore;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Question score", "The score given for answering a single question correctly."), GUILayout.Width(130));
			ParentEditor.GameConfig.QuestionScore = EditorGUILayout.IntField(ParentEditor.GameConfig.QuestionScore, GUILayout.Width(50));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.QuestionScore != oldQuestionScore)
			{
				isDirty = true;
			}

			var oldNumBronze = ParentEditor.GameConfig.NumQuestionsNeededForBronzeTrophy;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Bronze", "The number of correct questions needed to obtain a bronze trophy."), GUILayout.Width(130));
			ParentEditor.GameConfig.NumQuestionsNeededForBronzeTrophy = EditorGUILayout.IntField(ParentEditor.GameConfig.NumQuestionsNeededForBronzeTrophy, GUILayout.Width(30));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.NumQuestionsNeededForBronzeTrophy != oldNumBronze)
			{
				isDirty = true;
			}

			var oldNumSilver = ParentEditor.GameConfig.NumQuestionsNeededForSilverTrophy;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Silver", "The number of correct questions needed to obtain a silver trophy."), GUILayout.Width(130));
			ParentEditor.GameConfig.NumQuestionsNeededForSilverTrophy = EditorGUILayout.IntField(ParentEditor.GameConfig.NumQuestionsNeededForSilverTrophy, GUILayout.Width(30));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.NumQuestionsNeededForSilverTrophy != oldNumSilver)
			{
				isDirty = true;
			}

			var oldNumGold = ParentEditor.GameConfig.NumQuestionsNeededForGoldTrophy;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Gold", "The number of correct questions needed to obtain a gold trophy."), GUILayout.Width(130));
			ParentEditor.GameConfig.NumQuestionsNeededForGoldTrophy = EditorGUILayout.IntField(ParentEditor.GameConfig.NumQuestionsNeededForGoldTrophy, GUILayout.Width(30));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.NumQuestionsNeededForGoldTrophy != oldNumGold)
			{
				isDirty = true;
			}

			GUILayout.EndVertical();
		}

		private void DrawGameUiPanel()
		{
			GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(500));

			var style = new GUIStyle
			{
				fontSize = 20,
				fontStyle = FontStyle.Bold,
				normal = { textColor = Color.white }
			};
			EditorGUILayout.LabelField("Game UI prefabs", style);
			GUILayout.Space(20);

			var oldUi = ParentEditor.GameConfig.SingleChoiceQuestionUi;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Single choice", "The prefab to use for the UI of the single choice questions."), GUILayout.Width(170));
			ParentEditor.GameConfig.SingleChoiceQuestionUi = (GameObject)EditorGUILayout.ObjectField("", ParentEditor.GameConfig.SingleChoiceQuestionUi, typeof(GameObject), false, GUILayout.Width(250));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.SingleChoiceQuestionUi != oldUi)
			{
				isDirty = true;
			}

			oldUi = ParentEditor.GameConfig.SingleChoiceWithImageQuestionUi;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Single choice (with image)", "The prefab to use for the UI of the single choice questions with images."), GUILayout.Width(170));
			ParentEditor.GameConfig.SingleChoiceWithImageQuestionUi = (GameObject)EditorGUILayout.ObjectField("", ParentEditor.GameConfig.SingleChoiceWithImageQuestionUi, typeof(GameObject), false, GUILayout.Width(250));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.SingleChoiceWithImageQuestionUi != oldUi)
			{
				isDirty = true;
			}

			oldUi = ParentEditor.GameConfig.MultipleChoiceQuestionUi;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Multiple choice", "The prefab to use for the UI of the multiple choice questions."), GUILayout.Width(170));
			ParentEditor.GameConfig.MultipleChoiceQuestionUi = (GameObject)EditorGUILayout.ObjectField("", ParentEditor.GameConfig.MultipleChoiceQuestionUi, typeof(GameObject), false, GUILayout.Width(250));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.MultipleChoiceQuestionUi != oldUi)
			{
				isDirty = true;
			}

			oldUi = ParentEditor.GameConfig.MultipleChoiceWithImageQuestionUi;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Multiple choice (with image)", "The prefab to use for the UI of the multiple choice questions with images."), GUILayout.Width(170));
			ParentEditor.GameConfig.MultipleChoiceWithImageQuestionUi = (GameObject)EditorGUILayout.ObjectField("", ParentEditor.GameConfig.MultipleChoiceWithImageQuestionUi, typeof(GameObject), false, GUILayout.Width(250));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.MultipleChoiceWithImageQuestionUi != oldUi)
			{
				isDirty = true;
			}

			oldUi = ParentEditor.GameConfig.TrueFalseQuestionUi;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("True/false", "The prefab to use for the UI of the true/false questions."), GUILayout.Width(170));
			ParentEditor.GameConfig.TrueFalseQuestionUi = (GameObject)EditorGUILayout.ObjectField("", ParentEditor.GameConfig.TrueFalseQuestionUi, typeof(GameObject), false, GUILayout.Width(250));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.TrueFalseQuestionUi != oldUi)
			{
				isDirty = true;
			}

			oldUi = ParentEditor.GameConfig.TrueFalseWithImageQuestionUi;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("True/false (with image)", "The prefab to use for the UI of the true/false questions with images."), GUILayout.Width(170));
			ParentEditor.GameConfig.TrueFalseWithImageQuestionUi = (GameObject)EditorGUILayout.ObjectField("", ParentEditor.GameConfig.TrueFalseWithImageQuestionUi, typeof(GameObject), false, GUILayout.Width(250));
			GUILayout.EndHorizontal();
			if (ParentEditor.GameConfig.TrueFalseWithImageQuestionUi != oldUi)
			{
				isDirty = true;
			}

			GUILayout.EndVertical();
		}

		private void CreateCategoryList()
		{
			categoryList = SetupReorderableList("Categories", ParentEditor.GameConfig.Categories, ref currentCategory, (rect, x) =>
				{
					EditorGUI.LabelField(new Rect(rect.x, rect.y, 350, EditorGUIUtility.singleLineHeight), x.Name);
				},
				x => { currentCategory = x; },
				() =>
				{
					var category = ScriptableObject.CreateInstance<Category>();
					ParentEditor.GameConfig.Categories.Add(category);
					AssetDatabase.AddObjectToAsset(category, ParentEditor.GameConfig);
					EditorUtility.SetDirty(ParentEditor.GameConfig);
				},
				x =>
				{
					Object.DestroyImmediate(currentCategory, true);
					currentCategory = null;
					EditorUtility.SetDirty(ParentEditor.GameConfig);
				});
		}

		private void DrawCategory(Category category)
		{
			var originalWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 60;

			GUILayout.BeginVertical(EditorStyles.helpBox);

			var oldName = category.Name;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Name", "The name of this category."), GUILayout.Width(70));
			category.Name = EditorGUILayout.TextField(category.Name, GUILayout.Width(150));
			GUILayout.EndHorizontal();
			if (category.Name != oldName)
			{
				isDirty = true;
			}

			var oldSprite = category.Sprite;
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Sprite", "The sprite representing this category."), GUILayout.Width(64));
			category.Sprite = (Sprite)EditorGUILayout.ObjectField("", category.Sprite, typeof(Sprite), false, GUILayout.Width(70));
			GUILayout.EndHorizontal();
			if (category.Sprite != oldSprite)
			{
				isDirty = true;
			}

			GUILayout.EndVertical();

			EditorGUIUtility.labelWidth = originalWidth;
		}
	}
}
