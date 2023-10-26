namespace CollectIt.Domain.Shared.ErrorHandling
{
    public class ProcessResult
    {
        public bool IsSuccess { get; protected set; }
        public string ErrorMessage { get; protected set; }

        protected ProcessResult(bool isSuccess, string errorMessage = "")
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ProcessResult Success() => new(true);

        public static ProcessResult Fail(string errorMessage) => new(false, errorMessage);
    }
    public class ProcessResult<T> : ProcessResult
    {
        public T? Content { get; private set; }

        protected ProcessResult(bool isSuccess, T? content = default, string errorMessage = "")
            : base(isSuccess, errorMessage)
        {
            Content = content;
        }

        public static ProcessResult<T> Success(T content) => new(true, content);

        public static new ProcessResult<T> Fail(string errorMessage) => new(false, default, errorMessage);

        public static ProcessResult<T> Fail(T content, string errorMessage) => new(false, content, errorMessage);
    }

}
