// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TriviaQuizKit
{
    /// <summary>
    /// The screen where the player can select the game mode to play.
    /// </summary>
    public class GameModeSelectionScreen : MonoBehaviour
    {
        public ToggleButtonGroup QuestionTypeToggleGroup;
        public ToggleButtonGroup TimeModeToggleGroup;

        private QuestionType selectedQuestionType;
        private TimeMode selectedTimeMode;

        private void Start()
        {
            SetQuestionType(PlayerPrefs.GetInt("question_type"));
            SetTimeMode(PlayerPrefs.GetInt("time_mode"));
            QuestionTypeToggleGroup.SetToggle((int)selectedQuestionType);
            TimeModeToggleGroup.SetToggle((int)selectedTimeMode);
        }

        private void SetQuestionType(int type)
        {
            selectedQuestionType = (QuestionType)type;
            PlayerPrefs.SetInt("question_type", (int)selectedQuestionType);
        }

        private void SetTimeMode(int mode)
        {
            selectedTimeMode = (TimeMode)mode;
            PlayerPrefs.SetInt("time_mode", (int)selectedTimeMode);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("CategorySelection");
        }
    }
}
