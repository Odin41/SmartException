namespace Common.Models
{
    public class ResultState
    {
        public ResultState(string code, string message)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; init; }

        public string Code { get; init; }
    }
}