using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Core.Base
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string ErrorMessage { get; }

        public Result()
        {
            IsSuccess = true;
            ErrorMessage = null;
        }

        public Result(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new Result();

        public static Result Failure(string errorMessage) => new Result(errorMessage);
    }

    public class Result<T> : Result
    {

        public T Value { get; }

        private Result(T value)
        {
            Value = value;
        }

        private Result(T value, string errorMessage) : base(errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(value);

        public new static Result<T> Failure(string errorMessage) => new Result<T>(default, errorMessage);
    }
}
