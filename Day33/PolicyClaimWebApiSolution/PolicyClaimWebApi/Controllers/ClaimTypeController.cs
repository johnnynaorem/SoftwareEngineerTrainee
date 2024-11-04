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
    public class ClaimTypeController : ControllerBase
    {
        private readonly IClaimTypeService _claimTypeService;

        public ClaimTypeController(IClaimTypeService claimTypeService)
        {
            _claimTypeService = claimTypeService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePolicy(ClaimTypeDTO enitity)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var claimType = await _claimTypeService.CreateClaimType(enitity);
                    return Ok(claimType);
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

        [HttpGet("GetAllClaimTypes")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Claimant")]
        public async Task<IActionResult> GetAllClaimTypes()
        {
            try
            {
                var claimTypes = await _claimTypeService.GetAll();
                return Ok(claimTypes);

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

        [HttpGet("GetAllClaimTypesNames")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Claimant")]
        public async Task<IActionResult> GetAllClaimTypesNames()
        {
            try
            {
                var claimTypes = await _claimTypeService.GetAll();
                var claimTypesNames = (from names in claimTypes
                                       orderby names.TypeName
                                       select names.TypeName);

                return Ok(claimTypesNames);

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
