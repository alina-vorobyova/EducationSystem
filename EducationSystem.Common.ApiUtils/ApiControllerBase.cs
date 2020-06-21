using EducationSystem.Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Common.ApiUtils
{
    public class ApiControllerBase : ControllerBase
    {
        protected new IActionResult Ok()
        {
            return base.Ok(ApiResult.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(ApiResult.Ok(result));
        }

        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(ApiResult.Error(errorMessage));
        }

        protected IActionResult FromResult<T>(Result<T> result)
        {
            return result.IsSuccess ? Ok(result.Value) : Error(result.ErrorMessage);
        }

        protected IActionResult FromResult(Result result)
        {
            return result.IsSuccess ? Ok() : Error(result.ErrorMessage);
        }
    }
}