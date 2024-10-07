using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;
using PizzaStoreAPI.Repositories;

namespace PizzaStoreAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Cart> _cartRepo;
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly IRepository<int, Order> _orderRepo;

        public OrderService(IRepository<int, Cart> cartRepo, IRepository<int, Customer> customerRepo, IRepository<int, Order> orderRepo) 
        { 
            _cartRepo = cartRepo;
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
        }
        public async Task<Order> AddCartToOrder(int cartNumber, int customerId, string paymentMethod)
        {
            var custCart = await GetCustomerCart(customerId);
            var isCustHavethisCart = custCart.CartNumber == cartNumber;
            if (!isCustHavethisCart) 
            {
                throw new CollectionEmptyException("No Cart for Customer");
            }
            var carts = await _cartRepo.GetAll();
            int orderNumber = carts.Count() + 1;
            var totalAmount = await GetTotalAmount(custCart);

            Order order = new Order()
            {
                CustomerId = customerId,
                OrderNumber = cartNumber,
                TotalAmount = totalAmount,
                PaymentMethod = paymentMethod,
                IsPaymentSuccess = true,
                OrderStatus = OrderStatus.Created,
                Pizza = custCart.Pizzas
                
            };
            Console.WriteLine("From Order Service: " + order.OrderStatus);
            await _orderRepo.Add(order);
            await _cartRepo.Delete(cartNumber);
            return order;
        }

        async Task<float> GetTotalAmount(Cart custCart)
        {
            float totalPrice = 0;
            foreach(var pizza in custCart.Pizzas)
            {
                totalPrice += pizza.Price * pizza.Quantity;
            }
            return totalPrice;
        }
        async Task<Cart> GetCustomerCart(int customerId)
        {
            var carts = await _cartRepo.GetAll();
            return carts.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public async Task<IEnumerable<Order>> GetOrder(int customerId)
        {
            var order = await GetCustomerOrder(customerId);
            if (order == null)
            {
                throw new NoEntityFoundException("Order", customerId);
            }
            return order;

        }
        async Task<IEnumerable<Order>> GetCustomerOrder(int customerId)
        {
            var orders = await _orderRepo.GetAll();
            return orders.Where(order => order.CustomerId == customerId);
        }

        public async Task<Order> UpdateOrder(int customerId, int orderNumber, string paymentMethod)
        {
            var order = await _orderRepo.GetAll();
            var updateOrder = order.FirstOrDefault(o => o.OrderNumber == orderNumber && o.CustomerId == customerId);

            updateOrder.OrderStatus = OrderStatus.Processing;
            updateOrder.PaymentMethod = paymentMethod;

            return updateOrder;
        }
    }
}
