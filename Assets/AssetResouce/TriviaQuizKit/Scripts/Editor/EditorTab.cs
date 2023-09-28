// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// The base class used for the visual editor tabs.
    /// </summary>
    public class EditorTab
    {
        protected readonly TriviaQuizKitEditor ParentEditor;

        protected EditorTab(TriviaQuizKitEditor editor)
        {
            ParentEditor = editor;
        }

        public virtual void OnTabSelected()
        {
        }

        public virtual void Draw()
        {
        }

        protected static ReorderableList SetupReorderableList<T>(
            string headerText,
            List<T> elements,
            ref T currentElement,
            Action<Rect, T> drawElement,
            Action<T> selectElement,
            Action createElement,
            Action<T> removeElement)
        {
            var list = new ReorderableList(elements, typeof(T), true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, headerText); },
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = elements[index];
                    drawElement(rect, element);
                }
            };

            list.onSelectCallback = l =>
            {
                var selectedElement = elements[list.index];
                selectElement(selectedElement);
            };

            if (createElement != null)
            {
                list.onAddDropdownCallback = (buttonRect, l) =>
                {
                    createElement();
                };
            }

            list.onRemoveCallback = l =>
            {
                if (!EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete this item?", "Yes", "No")
                )
                {
                    return;
                }
                var element = elements[l.index];
                removeElement(element);
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
            };

            list.onChangedCallback = l =>
            {
                if (list.index >= 0 && list.index < list.count)
                {
                    var selectedElement = elements[list.index];
                    selectElement(selectedElement);
                }
            };

            return list;
        }
    }
}
