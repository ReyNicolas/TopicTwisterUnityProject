// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.Assertions;

namespace TriviaQuizKit
{
    /// <summary>
    /// Utility class to load a game configuration from the Resources folder.
    /// </summary>
    public static class GameConfigurationLoader
    {
        public static GameConfiguration LoadGameConfiguration(string configName)
        {
			var configObj = Resources.Load(configName);
            Assert.IsNotNull(configObj);
            return configObj as GameConfiguration;
        }
    }
}
