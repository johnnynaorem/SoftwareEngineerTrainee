using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Validations
{
    public class MovieRentExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ErrorResponseDTO
            {
                ErrorCode = 500,
                ErrorMessage = context.Exception.Message
            });
        }
    }
}
