namespace Application.DTOS.Utils
{
    public class OperationResult
    {
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        private OperationResult(bool succeeded, string message, IEnumerable<string> errors = null)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public static OperationResult Success(string message) => new OperationResult(true, message);
        public static OperationResult Failure(string message, IEnumerable<string> errors = null) => new OperationResult(false, message, errors);
    }
}
