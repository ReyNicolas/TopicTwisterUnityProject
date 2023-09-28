// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEditor;
using UnityEngine;

namespace TriviaQuizKit
{
    /// <summary>
    /// The 'About' tab in the visual editor.
    /// </summary>
    public class AboutTab : EditorTab
    {
        private const string PurchaseText = "Thank you for your purchase!";
        private const string CopyrightText =
            "Trivia Quiz Kit is brought to you by gamevanilla. Copyright (C) gamevanilla 2018.";
        private const string WikiUrl = "https://www.wiki.gamevanilla.com/index.php?title=Trivia_Quiz_Kit";
        private const string SupportUrl = "https://www.gamevanilla.com";
        private const string EulaUrl = "https://unity3d.com/es/legal/as_terms";
        private const string AssetStoreUrl = "https://www.assetstore.unity3d.com/#!/content/119432";

        private readonly Texture2D logoTexture;

        public AboutTab(TriviaQuizKitEditor editor) : base(editor)
        {
            logoTexture = Resources.Load<Texture2D>("Editor/Logo");
        }

        public override void Draw()
        {
            GUILayout.Space(15);

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(logoTexture);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.Space(15);

            var windowWidth = EditorWindow.focusedWindow.position.width;
            var centeredLabelStyle = new GUIStyle("label") {alignment = TextAnchor.MiddleCenter};
            GUI.Label(new Rect(0, 0, windowWidth, 650), PurchaseText, centeredLabelStyle);
            GUI.Label(new Rect(0, 0, windowWidth, 700), CopyrightText, centeredLabelStyle);
            var centeredButtonStyle = new GUIStyle("button") {alignment = TextAnchor.MiddleCenter};
            if (GUI.Button(new Rect(windowWidth / 2 - 100 / 2.0f, 400, 100, 50), "Documentation", centeredButtonStyle))
            {
                Application.OpenURL(WikiUrl);
            }

            if (GUI.Button(new Rect(windowWidth / 2 - 100 / 2.0f, 460, 100, 50), "Support", centeredButtonStyle))
            {
                Application.OpenURL(SupportUrl);
            }

            if (GUI.Button(new Rect(windowWidth / 2 - 100 / 2.0f, 520, 100, 50), "License", centeredButtonStyle))
            {
                Application.OpenURL(EulaUrl);
            }

            if (GUI.Button(new Rect(windowWidth / 2 - 100 / 2.0f, 580, 100, 50), "Rate me!", centeredButtonStyle))
            {
                Application.OpenURL(AssetStoreUrl);
            }
        }
    }
}
