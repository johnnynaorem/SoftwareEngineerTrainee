using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Exceptions;
using PizzaStore.Interfaces;

namespace PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewPizzas()
        {
            try
            {
                var pizzas = await _customerService.ViewPizzas();
                return Ok(pizzas);
            }
            catch (CollectionEmptyException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
