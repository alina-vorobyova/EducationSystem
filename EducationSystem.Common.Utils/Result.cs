using System.Text;

namespace EducationSystem.Common.Utils
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public string ErrorMessage { get; private set; }

        public Result()
        {
            IsSuccess = true;
            ErrorMessage = string.Empty;
        }

        public Result(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new Result();
        public static Result<T> Success<T>(T value) => new Result<T>(value);
        public static Result Failure(string errorMessage) => new Result(errorMessage);
        public static Result<T> Failure<T>(string errorMessage) => new Result<T>(default!, errorMessage);

        public static Result Combine(params Result[] results)
        {
            var combinedResult = new Result();
            var sb = new StringBuilder();

            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    combinedResult.IsSuccess = false;

                    if (sb.Length > 0)
                        sb.Append(' ');

                    sb.Append(result.ErrorMessage);
                }
            }

            if (combinedResult.IsFailure)
                combinedResult.ErrorMessage = sb.ToString();

            return combinedResult;
        }
    }

    public class Result<T> : Result
    {

        public T Value { get; }

        public Result(T value)
        {
            Value = value;
        }

        public Result(T value, string errorMessage) : base(errorMessage)
        {
            Value = value;
        }
    }
}
