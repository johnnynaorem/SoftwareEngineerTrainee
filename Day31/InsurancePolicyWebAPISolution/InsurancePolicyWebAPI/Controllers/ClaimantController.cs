using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimantController : ControllerBase
    {
        private readonly IClaimantService _service;

        public ClaimantController(IClaimantService service)
        {
            _service = service;
        }

        [HttpGet("GetAllClaimant")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var claimants = await _service.GetAll();
                return Ok(claimants);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreateClaimant")]
        public async Task<IActionResult> Create(ClaimantDTO entity)
        {
            try
            {
                var newclaimant = await _service.CreateClaiment(entity); 
                return Ok(newclaimant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
