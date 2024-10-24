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
        public async Task<IActionResult> CreateClaimWithFiles([FromForm] CreateClaimDTO createClaimDto)
        {
            //if (createClaimDto.Files == null || createClaimDto.Files.Count == 0)
            //{
            //    return BadRequest("At least one file must be uploaded.");
            //}

            var claim = await _claimService.CreateClaim(createClaimDto);
            return Ok(new { ClaimId = claim.ClaimID, Files = createClaimDto.Files.Count });
        }
    }
}
