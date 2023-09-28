// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace TriviaQuizKit
{
	/// <summary>
	/// The multiple choice question type.
	/// </summary>
	[Serializable]
	public class MultipleChoiceQuestion : BaseQuestion
	{
		public Sprite Image;
		public int NumCorrectAnswers;
		public List<string> Answers = new List<string>();

#if UNITY_EDITOR
		private ReorderableList answerList;
		private string currentAnswer;
#endif

		public void OnEnable()
		{
#if UNITY_EDITOR
            answerList = new ReorderableList(Answers, typeof(string), true, true, true, true)
            {
                drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Answers"); },
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var answer = Answers[index];
					EditorGUI.LabelField(new Rect(rect.x, rect.y, 350, EditorGUIUtility.singleLineHeight), EditorUtils.GetEditorFriendlyText(answer));
                }
            };

            answerList.onSelectCallback = l =>
            {
                var selectedAnswer = Answers[l.index];
	            currentAnswer = selectedAnswer;
            };

			answerList.onAddDropdownCallback = (buttonRect, l) =>
			{
				Answers.Add("Placeholder answer");
				EditorUtility.SetDirty(this);
			};

            answerList.onRemoveCallback = l =>
            {
                currentAnswer = null;
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
            };
#endif
		}

#if UNITY_EDITOR
		public override void Draw()
		{
			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Question", "The text of this question."), GUILayout.Width(70));
			EditorStyles.textField.wordWrap = true;
			Question = EditorGUILayout.TextArea(Question, GUILayout.Width(170), GUILayout.Height(75));
			GUILayout.EndHorizontal();

			GUILayout.Space(10);

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Image", "The (optional) image of this question."), GUILayout.Width(64));
			Image = (Sprite)EditorGUILayout.ObjectField("", Image, typeof(Sprite), false, GUILayout.Width(70));
			GUILayout.EndHorizontal();

			GUILayout.Space(10);

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Corrects", "The number of correct answers of this question."), GUILayout.Width(64));
			NumCorrectAnswers = EditorGUILayout.IntField(NumCorrectAnswers, GUILayout.Width(30));
			GUILayout.EndHorizontal();

			GUILayout.Space(10);

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical(GUILayout.Width(250));
			answerList?.DoLayoutList();
			GUILayout.EndVertical();

			if (currentAnswer != null)
			{
				var originalWidth = EditorGUIUtility.labelWidth;
				EditorGUIUtility.labelWidth = 60;

				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(new GUIContent("Answer", "The text of the answer."), GUILayout.Width(70));
				EditorStyles.textField.wordWrap = true;
				var idx = Answers.FindIndex(x => x == currentAnswer);
				currentAnswer = EditorGUILayout.TextArea(currentAnswer, GUILayout.Width(150), GUILayout.Height(50));
				if (idx != -1)
				{
					Answers[idx] = currentAnswer;
				}
				GUILayout.EndHorizontal();

				EditorGUIUtility.labelWidth = originalWidth;
			}

			GUILayout.EndHorizontal();

			GUILayout.Space(10);

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Metadata", "The metadata associated to this question."), GUILayout.Width(70));
			EditorStyles.textField.wordWrap = true;
			Metadata = EditorGUILayout.TextArea(Metadata, GUILayout.Width(170), GUILayout.Height(75));
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			EditorUtility.SetDirty(this);
		}
#endif
	}
}
