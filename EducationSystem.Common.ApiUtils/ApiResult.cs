using System;

namespace EducationSystem.Common.ApiUtils
{
    public class ApiResult<T>
    {
        public T Result { get; }
        public string ErrorMessage { get; }
        public DateTime TimeGenerated { get; }

        protected internal ApiResult(T result, string errorMessage)
        {
            Result = result;
            ErrorMessage = errorMessage;
            TimeGenerated = DateTime.UtcNow;
        }
    }

    public sealed class ApiResult : ApiResult<string>
    {
        private ApiResult(string errorMessage)
            : base(default!, errorMessage)
        {
        }

        public static ApiResult<T> Ok<T>(T result)
        {
            return new ApiResult<T>(result, string.Empty);
        }

        public static ApiResult Ok()
        {
            return new ApiResult(string.Empty);
        }

        public static ApiResult Error(string errorMessage)
        {
            return new ApiResult(errorMessage);
        }
    }
}