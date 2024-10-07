using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;

namespace PizzaStoreAPI.Interfaces
{
    public interface IOrderDetailService
    {
        public Task<OrderDetails> AddOrderToOrderDetail(int customerId);
        public Task<OrderDetails> GetAllOrderDetails(int SiNo);
    }
}
