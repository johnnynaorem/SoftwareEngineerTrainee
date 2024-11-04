using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimReport : ControllerBase
    {
        private readonly IClaimReport _service;

        public ClaimReport(IClaimReport service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromForm] ClaimReportRequest request)
        {
            try
            {
                var result = await _service.UploadFileAsync(request.FilePath, request);
                return Ok("Report Created");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
