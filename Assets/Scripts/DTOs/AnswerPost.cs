using System;

namespace DTOs
{
    [Serializable]
    public class AnswerPost
    {
        public string CategoryName;
        public string Word;
        public char Letter;

        public AnswerPost(string categoryName, string word, char letter)
        {
            CategoryName = categoryName;
            Word = word;
            Letter = letter;
        }
    }
}