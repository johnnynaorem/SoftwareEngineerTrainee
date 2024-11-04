using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Validations
{
    public class ClaimExceptionFilter:ExceptionFilterAttribute
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
