using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpPost]
        [Authorize(Roles = "Claimant")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateClaimWithFiles([FromForm] CreateClaimDTO createClaimDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createClaimDto.Files == null || createClaimDto.Files.Count == 0)
                    {
                        return BadRequest("At least one file must be uploaded.");
                    }

                    var claim = await _claimService.CreateClaim(createClaimDto);
                    return Ok(new { ClaimId = claim.ClaimID, Files = createClaimDto.Files.Count });
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateClaimStatus(UpdateClaimStatusDTO updateClaimStatus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var claim = await _claimService.UpdateClaimStatus(updateClaimStatus);
                    return Ok(new { ClaimId = claim.ClaimID});
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
