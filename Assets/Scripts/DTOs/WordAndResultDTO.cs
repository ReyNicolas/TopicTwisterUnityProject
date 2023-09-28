
namespace DTOs
{
    public class WordAndResultDTO
    {
        public readonly string Word;
        public readonly bool IsCorrect;

        public WordAndResultDTO(string word, bool isCorrect)
        {
            Word = word;
            IsCorrect = isCorrect;
        }
    }
}