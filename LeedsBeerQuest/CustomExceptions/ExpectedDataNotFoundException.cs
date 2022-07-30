namespace LeedsBeerQuest.CustomExceptions
{
    public class ExpectedDataNotFoundException : Exception
    {
        public ExpectedDataNotFoundException(string message) : base(message) { }
    }
}
