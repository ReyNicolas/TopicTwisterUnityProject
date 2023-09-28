// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

namespace TriviaQuizKit
{
	/// <summary>
	/// Miscellaneous editor utility methods.
	/// </summary>
	public static class EditorUtils
	{
		public static string GetEditorFriendlyText(string original, int maxQuestionLen = 32)
		{
			if (original.Length > maxQuestionLen)
			{
				return original.Substring(0, maxQuestionLen) + "...";
			}

			return original;
		}
	}
}
