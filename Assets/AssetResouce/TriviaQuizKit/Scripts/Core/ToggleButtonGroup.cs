// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// Helper class to group several related toggle buttons.
    /// </summary>
    public class ToggleButtonGroup : MonoBehaviour
    {
        public List<ToggleButton> Buttons;

        public void SetToggle(ToggleButton toggledButton)
        {
            foreach (var button in Buttons)
            {
                button.SetToggled(button == toggledButton);
            }
        }

        public void SetToggle(int toggledButton)
        {
            for (var i = 0; i < Buttons.Count; i++)
            {
                var button = Buttons[i];
                button.SetToggled(i == toggledButton);
            }
        }
    }
}
