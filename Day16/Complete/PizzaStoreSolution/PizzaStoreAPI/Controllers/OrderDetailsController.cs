using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;
using PizzaStoreAPI.Services;
using System.Diagnostics;

namespace PizzaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService) 
        { 
            _orderDetailService = orderDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderDetails(int orderNumber)
        {
            try
            {
                var cart = await _orderDetailService.AddOrderToOrderDetail(orderNumber);
                return Ok(cart);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(int Sl)
        {
            try
            {
                var order = await _orderDetailService.GetAllOrderDetails(Sl);
                return Ok(order);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }
    }
}
