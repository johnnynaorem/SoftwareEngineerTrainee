using PizzaStore.Exceptions;
using PizzaStore.Interfaces;
using PizzaStore.Models;

namespace PizzaStore.Repositories
{
    public class CartRepository : IRepository<int, Cart>
    {
        List<Cart> orders = new List<Cart>();

        public Task<Cart> Add(Cart entity)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> Update(Cart entity)
        {
            throw new NotImplementedException();
        }

        Task<Cart> IRepository<int, Cart>.Delete(int key)
        {
            throw new NotImplementedException();
        }

        Task<Cart> IRepository<int, Cart>.Get(int key)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Cart>> IRepository<int, Cart>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
