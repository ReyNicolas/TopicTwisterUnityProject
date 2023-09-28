// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TriviaQuizKit
{
	/// <summary>
	/// The true or false question type.
	/// </summary>
	public class TrueFalseQuestion : BaseQuestion
	{
		public Sprite Image;
		public bool IsTrue;

#if UNITY_EDITOR
		public override void Draw()
		{
			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Question", "The text of this question."), GUILayout.Width(70));
			EditorStyles.textField.wordWrap = true;
			Question = EditorGUILayout.TextArea(Question, GUILayout.Width(170), GUILayout.Height(75));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Image", "The (optional) image of this question."), GUILayout.Width(64));
			Image = (Sprite)EditorGUILayout.ObjectField("", Image, typeof(Sprite), false, GUILayout.Width(70));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Is true", "True if this question is true; false otherwise."), GUILayout.Width(70));
			IsTrue = EditorGUILayout.Toggle(IsTrue);
			GUILayout.EndHorizontal();

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
