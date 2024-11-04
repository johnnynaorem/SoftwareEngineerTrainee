using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;

namespace PizzaStoreAPI.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> AddCartToOrder(int cartNumber, int customerId, string paymentMethod);
        public Task<IEnumerable<Order>> GetOrder(int customerId);
        public Task<Order> UpdateOrder(int customerId, int orderNumber, string paymentMethod);
    }
}
