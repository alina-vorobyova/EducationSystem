using System;
using Microsoft.AspNetCore.Http;

namespace EducationSystem.Common.ApiUtils
{
    public class ApiResult<T>
    {
        public T Result { get; }
        public string ErrorMessage { get; }
        public DateTime TimeGenerated { get; }
        public int StatusCode { get; set; }

        public ApiResult(T result, string errorMessage, int statusCode)
        {
            Result = result;
            ErrorMessage = errorMessage;
            TimeGenerated = DateTime.UtcNow;
            StatusCode = statusCode;
        }
    }

    public sealed class ApiResult : ApiResult<string>
    {
        public ApiResult(string errorMessage, int statusCode)
            : base(default!, errorMessage, statusCode)
        {
        }

        public static ApiResult<T> Ok<T>(T result)
        {
            return new ApiResult<T>(result, string.Empty, StatusCodes.Status200OK);
        }

        public static ApiResult Ok()
        {
            return new ApiResult(string.Empty, StatusCodes.Status200OK);
        }

        public static ApiResult Error(string errorMessage)
        {
            return new ApiResult(errorMessage, StatusCodes.Status400BadRequest);
        }
    }
}