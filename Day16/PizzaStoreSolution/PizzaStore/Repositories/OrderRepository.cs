using PizzaStore.Exceptions;
using PizzaStore.Interfaces;
using PizzaStore.Models;

namespace PizzaStore.Repositories
{
    public class OrderRepository : IRepository<int, Order>
    {
        List<Order> orders = new List<Order>();
        public async Task<Order> Add(Order entity)
        {
            entity.OrderNumber = orders.Count + 1;
            orders.Add(entity);
            return entity;
        }

        public async Task<Order> Delete(int key)
        {
            var order = await Get(key);
            orders.Remove(order);
            return order;
        }

        public async Task<Order> Get(int key)
        {
            var order = orders.FirstOrDefault(o=> o.OrderNumber == key);
            if(order == null)
            {
                throw new NoEntityFoundException("Order", key);
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            if(orders.Count == 0)
            {
                throw new NoEntityFoundException("Order");
            }
            return orders;
        }

        public async Task<Order> Update(Order entity)
        {
            var order = await Get(entity.OrderNumber);
            order.PaymentMethod = entity.PaymentMethod;
            order.OrderStatus = entity.OrderStatus;
            order.IsPaymnetSuccess = entity.IsPaymnetSuccess;

            return order;
            
        }
    }
}
