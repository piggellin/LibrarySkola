
namespace Application
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }
        public string SuccessMessage { get; }

        private Result(bool isSuccess, T value, string error, string successMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            SuccessMessage = successMessage;
        }

        public static Result<T> Success(T value, string successMessage = null)
            => new Result<T>(true, value, null, successMessage);

        public static Result<T> Failure(string error)
            => new Result<T>(false, default, error, null);
    }
}
