using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models.DTOs;
using PizzaStoreAPI.Services;
using System.Diagnostics;

namespace PizzaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartToOrder(int cartNumber, int customerId, string paymentMethod)
        {
            try
            {
                var cart = await _orderService.AddCartToOrder(cartNumber, customerId, paymentMethod);
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
        public async Task<IActionResult> GetOrder(int customerId)
        {
            try
            {
                var order = await _orderService.GetOrder(customerId);
                return Ok(order);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(int customerId, int orderNumber, string paymentMethod)
        //{
        //    try
        //    {
        //        var order = await _orderService.UpdateOrder(customerId, orderNumber, paymentMethod);
        //        return Ok(order);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //        Debug.WriteLine(e.StackTrace);
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
