// Copyright (C) 2018-2020 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TriviaQuizKit
{
	/// <summary>
	/// The screen where the player can select the category to play.
	/// </summary>
	public class CategorySelectionScreen : MonoBehaviour
	{
        public ToggleButtonGroup CategoryToggleGroup;
		public GameObject CategoryTogglePrefab;

        private int selectedCategory;
		private CategoryToggleWrapper anyToggle;

		private void Start()
		{
			var categoryDb = GameConfigurationLoader.LoadGameConfiguration("GameConfiguration");
			if (categoryDb != null)
			{
				var i = 0;
				foreach (var category in categoryDb.Categories)
				{
					var toggle = CreateCategoryToggle(category.Name, category.Sprite, i);
					CategoryToggleGroup.Buttons.Add(toggle.ToggleButton);
					++i;
				}

				anyToggle = CreateCategoryToggle("Any", categoryDb.AnyCategorySprite, -1);
				CategoryToggleGroup.Buttons.Add(anyToggle.ToggleButton);
			}

			SetCategory(PlayerPrefs.GetInt("category"));
            CategoryToggleGroup.SetToggle(selectedCategory != -1 ? selectedCategory : CategoryToggleGroup.Buttons.Count - 1);
		}

        private void SetCategory(int category)
        {
		    selectedCategory = category;
	        PlayerPrefs.SetInt("category", selectedCategory);
        }

		public void OnBackButtonPressed()
		{
			SceneManager.LoadScene("ModeSelection");
		}

		public void OnNextButtonPressed()
		{
			SceneManager.LoadScene("Game");
		}

		private CategoryToggleWrapper CreateCategoryToggle(string categoryName, Sprite categorySprite, int categoryIndex)
		{
			var toggle = Instantiate(CategoryTogglePrefab).GetComponent<CategoryToggleWrapper>();
			toggle.transform.SetParent(CategoryToggleGroup.transform, false);
			toggle.CategoryToggle.CategoryName.text = categoryName;
			toggle.CategoryToggle.CategoryImage.sprite = categorySprite;
			toggle.ToggleButton.ToggleGroup = CategoryToggleGroup;
			toggle.FlatButton.OnPressedEvent.AddListener(() => { SetCategory(categoryIndex); });
			return toggle;
		}
	}
}
