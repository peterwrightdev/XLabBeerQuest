namespace LeedsBeerQuest.CustomExceptions
{
    public class InvalidReviewException : Exception
    {
        public InvalidReviewException(string message) : base(message) { }
    }
}
