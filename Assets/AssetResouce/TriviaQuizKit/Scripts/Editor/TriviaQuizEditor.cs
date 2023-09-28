// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// The included visual editor available from the Window/Trivia Quiz Kit/Editor menu option.
    /// </summary>
    public class TriviaQuizKitEditor : EditorWindow
    {
        public GameConfiguration GameConfig;

        private readonly List<EditorTab> tabs = new List<EditorTab>();
        private int selectedTabIndex;
        private int prevSelectedTabIndex;
        private Vector2 scrollPos;

        [MenuItem("Tools/Trivia Quiz Kit/Editor", false, 0)]
        private static void Init()
        {
            var window = GetWindow(typeof(TriviaQuizKitEditor));
            window.titleContent = new GUIContent("Trivia Quiz Kit Editor");
        }

        private void OnEnable()
        {
            tabs.Add(new GameConfigurationTab(this));
            tabs.Add(new QuestionsTab(this));
            tabs.Add(new AboutTab(this));
            selectedTabIndex = 0;
        }

        private void OnGUI()
        {
            selectedTabIndex = GUILayout.Toolbar(selectedTabIndex,
                new[] {"Game configuration", "Questions", "About"});
            if (selectedTabIndex >= 0 && selectedTabIndex < tabs.Count)
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                var selectedEditor = tabs[selectedTabIndex];
                if (selectedTabIndex != prevSelectedTabIndex)
                {
                    selectedEditor.OnTabSelected();
                    GUI.FocusControl(null);
                }
                selectedEditor.Draw();
                prevSelectedTabIndex = selectedTabIndex;
                EditorGUILayout.EndScrollView();
            }
        }
    }
}
