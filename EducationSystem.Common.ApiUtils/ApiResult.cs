using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EducationSystem.Common.ApiUtils
{
    public class ApiResult<T>
    {
        public T Result { get; }
        public int StatusCode { get; }
        public DateTime TimeGenerated { get; }
        public List<string> Errors { get; }

        public ApiResult(T result, int statusCode, params string[] errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.UtcNow;
            StatusCode = statusCode;
        }
    }

    public sealed class ApiResult : ApiResult<string>
    {
        public ApiResult(int statusCode, params string[] errors)
            : base(default!, statusCode, errors)
        {
        }

        public ApiResult(ModelStateDictionary modelState) 
            : base(default!, StatusCodes.Status400BadRequest)
        {
            foreach (var item in modelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    Errors.Add(error.ErrorMessage);
                }
            }
        }

        public static ApiResult<T> Ok<T>(T result)
        {
            return new ApiResult<T>(result, StatusCodes.Status200OK);
        }

        public static ApiResult Ok()
        {
            return new ApiResult(StatusCodes.Status200OK);
        }

        public static ApiResult Error(params string[] errors)
        {
            return new ApiResult(StatusCodes.Status400BadRequest, errors);
        }
    }
}