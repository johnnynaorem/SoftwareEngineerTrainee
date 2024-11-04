using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models.DTOs;

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

        [HttpPost("createPolicy")]
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

        [HttpGet("GetAllPolicies")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Claimant")]
        public async Task<IActionResult> GetAllPolicies()
        {
            try
            {
                var policies = await _policyService.GetAll();
                return Ok(policies);

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

        [HttpGet("GetAllPoliciesNames")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Claimant")]
        public async Task<IActionResult> GetAllPoliciesNames()
        {
            try
            {
                var policies = await _policyService.GetAll();
                var policiesNames = (
                                        from names in policies
                                        orderby names.AboutPolicy
                                        select names.AboutPolicy
                                     );

                return Ok(policiesNames);

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
