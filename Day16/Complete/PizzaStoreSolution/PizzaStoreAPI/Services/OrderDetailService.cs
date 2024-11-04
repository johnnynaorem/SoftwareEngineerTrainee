using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;
using PizzaStoreAPI.Repositories;

namespace PizzaStoreAPI.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepository<int, Order> _orderRepo;
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly IRepository<int, Pizza> _pizzaRepo;
        private readonly IRepository<int, OrderDetails> _orderDetailsRepo;

        public OrderDetailService(IRepository<int, Customer> customerRepo, IRepository<int, Order> orderRepo, IRepository<int, OrderDetails> orderDetailRepo) 
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _orderDetailsRepo = orderDetailRepo;
        }

        public async Task<OrderDetails> AddOrderToOrderDetail(int customerId)
        {
            var orders = await _orderRepo.Get(customerId);
            //var custOrder = orders.Where(o => o.CustomerId == customerId);
            if (await DoesCustomerHaveThisOrderDetails(customerId) || orders == null)
            {
                throw new CollectionEmptyException("order");
            }
            var OrderDetail = new OrderDetails
            {
                OrderNumber = orders.OrderNumber,
                Pizza = orders.Pizza,
                //PizzaId = pizzaCartDTO.PizzaId,
                //Quantity = pizzaCartDTO.Quantity,
                //Price = pizzaCartDTO.Price,
                //Discount = pizzaCartDTO.Discount,
            };

            await _orderDetailsRepo.Add(OrderDetail);
            return OrderDetail;
        }

        async Task<bool> DoesCustomerHaveThisOrderDetails(int orderNumber)
        {
            try
            {
                var orderDetails = await _orderDetailsRepo.GetAll();
                return orderDetails.Any(o => o.OrderNumber == orderNumber);
            }
            catch (CollectionEmptyException)
            {
                return false;
            }
        }

        async Task<OrderDetails> IOrderDetailService.GetAllOrderDetails(int sl)
        {
            var orderDetails = await _orderDetailsRepo.Get(sl);
            return orderDetails;
        }
    }
}
