using PizzaStore.Interfaces;
using PizzaStore.Models;

namespace PizzaStore.Repositories
{
    public class OrderDetailsRepository : IRepository<int, OrderDetails>
    {
        List<OrderDetails> orderDetails = new List<OrderDetails>()
        {
            new OrderDetails(){SlNo=100, OrderNumber=200, PizzaId=1, Quantity=4, Price=190, Discount=10},
            new OrderDetails(){SlNo=101, OrderNumber=201, PizzaId=2, Quantity=5, Price=290, Discount=20},
        };

        public async Task<OrderDetails> Add(OrderDetails entity)
        {
            entity.SlNo = orderDetails.Max(c => c.SlNo) + 1;
            orderDetails.Add(entity);
            return entity;
        }

        public Task<OrderDetails> Delete(int key)
        {
            
        }

        public Task<OrderDetails> Get(int key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDetails>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails> Update(OrderDetails entity)
        {
            throw new NotImplementedException();
        }
    }
}
