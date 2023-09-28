// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace TriviaQuizKit
{
    /// <summary>
    /// Utility class to load a question pack from the Resources folder.
    /// </summary>
    public static class QuestionPackLoader
    {
        public static List<BaseQuestion> LoadQuestions(string packName)
        {
			var packObj = Resources.Load(packName);
            Assert.IsNotNull(packObj);
            Assert.IsTrue(packObj is QuestionPack);
            return (packObj as QuestionPack).Questions;
        }

        public static List<BaseQuestion> LoadAllQuestions(GameConfiguration gameConfig)
        {
            var questions = new List<BaseQuestion>();
            foreach (var pack in gameConfig.PreloadedQuestionPacks.Packs)
            {
                foreach (var question in pack.Questions)
                {
                    questions.Add(question);
                }
            }
            return questions;
        }
    }
}
