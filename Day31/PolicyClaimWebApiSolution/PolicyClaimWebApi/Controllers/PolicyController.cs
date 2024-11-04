using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Services;

namespace PolicyClaimWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePolicyWithTypes(PolicyDTO enitity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var policy = await _policyService.CreatePolicy(enitity);
                    return Ok(policy);
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = "one or more fields validate error"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 500
                });
            }
        }
    }
}
