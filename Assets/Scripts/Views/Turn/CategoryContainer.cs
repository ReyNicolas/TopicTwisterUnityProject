using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryContainer: MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI categoryText;
    [SerializeField] private Text wordText;
    [SerializeField] private Image answerImage;
    [SerializeField] private List<Sprite> sprites;

    public void SetCategoryText(string text)
    {
        categoryText.text = text;
    }

    public string GetCategoryText()
    {
        return categoryText.text;
    }


    public string GetWordText()
    {
        return wordText.text;
    }

    public void ShowResult(bool wordsResult)
    {
        answerImage.gameObject.SetActive(true);
        if (wordsResult == true)
        {
            answerImage.sprite = sprites[0];
        }
        else
        {
            answerImage.sprite = sprites[1];
        }
    }
}