// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEditor;
using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// The custom inspector for the GameScreen class.
    /// </summary>
    [CustomEditor(typeof(GameScreen), true)]
    public class GameScreenInspector : Editor
    {
        /// <summary>
        /// Unity's OnInspectorGUI method.
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUIUtility.labelWidth = 180;

            GUILayout.Label("Game settings", EditorStyles.boldLabel);

            var questionOrder = serializedObject.FindProperty("QuestionOrder");
            EditorGUILayout.PropertyField(questionOrder, new GUIContent("Question order"), GUILayout.MaxWidth(300));

            var questionPackLoad = serializedObject.FindProperty("QuestionPackLoad");
            EditorGUILayout.PropertyField(questionPackLoad, new GUIContent("Question pack to load"), GUILayout.MaxWidth(300));

            var questionPackLoadType = (GameScreen.QuestionPackLoadType)questionPackLoad.intValue;
            switch (questionPackLoadType)
            {
                case GameScreen.QuestionPackLoadType.All:
                    break;

                case GameScreen.QuestionPackLoadType.Single:
                    var questionPackToLoadName = serializedObject.FindProperty("QuestionPackToLoadName");
                    EditorGUILayout.PropertyField(questionPackToLoadName, new GUIContent("Question pack name"), GUILayout.MaxWidth(400));
                    break;
            }

            GUILayout.Space(10);

            GUILayout.Label("UI", EditorStyles.boldLabel);

            const float uiFieldWidth = 470;

            var canvas = serializedObject.FindProperty("Canvas");
            EditorGUILayout.PropertyField(canvas, new GUIContent("Canvas"), GUILayout.MaxWidth(uiFieldWidth));

            var numCorrectAnswersText = serializedObject.FindProperty("NumCorrectAnswersText");
            EditorGUILayout.PropertyField(numCorrectAnswersText, new GUIContent("NumCorrectAnswersText"), GUILayout.MaxWidth(uiFieldWidth));

            var numAnsweredText = serializedObject.FindProperty("NumAnsweredText");
            EditorGUILayout.PropertyField(numAnsweredText, new GUIContent("NumAnsweredText"), GUILayout.MaxWidth(uiFieldWidth));

            var scoreText = serializedObject.FindProperty("ScoreText");
            EditorGUILayout.PropertyField(scoreText, new GUIContent("ScoreText"), GUILayout.MaxWidth(uiFieldWidth));

            var numRemainingQuestionsText = serializedObject.FindProperty("NumRemainingQuestionsText");
            EditorGUILayout.PropertyField(numRemainingQuestionsText, new GUIContent("NumRemainingQuestionsText"), GUILayout.MaxWidth(uiFieldWidth));

            var categoryText = serializedObject.FindProperty("CategoryText");
            EditorGUILayout.PropertyField(categoryText, new GUIContent("CategoryText"), GUILayout.MaxWidth(uiFieldWidth));

            var countdownText = serializedObject.FindProperty("CountdownText");
            EditorGUILayout.PropertyField(countdownText, new GUIContent("CountdownText"), GUILayout.MaxWidth(uiFieldWidth));

            var countdownBgImage = serializedObject.FindProperty("CountdownBgImage");
            EditorGUILayout.PropertyField(countdownBgImage, new GUIContent("CountdownBgImage"), GUILayout.MaxWidth(uiFieldWidth));

            var countdownImage = serializedObject.FindProperty("CountdownImage");
            EditorGUILayout.PropertyField(countdownImage, new GUIContent("CountdownImage"), GUILayout.MaxWidth(uiFieldWidth));

            var noTimeLimitImage = serializedObject.FindProperty("NoTimeLimitImage");
            EditorGUILayout.PropertyField(noTimeLimitImage, new GUIContent("NoTimeLimitImage"), GUILayout.MaxWidth(uiFieldWidth));

            var selectButton = serializedObject.FindProperty("SelectButton");
            EditorGUILayout.PropertyField(selectButton, new GUIContent("SelectButton"), GUILayout.MaxWidth(uiFieldWidth));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
