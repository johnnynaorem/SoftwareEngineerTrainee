using PizzaStore.Exceptions;
using PizzaStore.Interfaces;
using PizzaStore.Models;

namespace PizzaStore.Repositories
{
    public class PizzaRepository : IRepository<int, Pizza>
    {
        List<Pizza> pizzas = new List<Pizza>()
        {
            new Pizza(){Id=1, Name="Pizza1", Description="john@gmail.com", Image="", Price=100, Quantity=5},
            new Pizza(){Id=2, Name="Pizza2", Description="jane@gmail.com", Image="", Price=100, Quantity=10},
        };

        public async Task<Pizza> Add(Pizza entity)
        {
            entity.Id = pizzas.Max(c => c.Id) + 1;
            pizzas.Add(entity);
            return entity;
        }

        public async Task<Pizza> Delete(int key)
        {
            var pizza = await Get(key);
            pizzas.Remove(pizza);
            return pizza;
        }

        public async Task<Pizza> Get(int key)
        {
            var pizza = pizzas.FirstOrDefault(p => p.Id == key);
            if(pizza == null)
            {
                throw new NoEntityFoundException("Pizza", key);
            }
            return pizza;
        }

        public async Task<IEnumerable<Pizza>> GetAll()
        {
            if (pizzas.Count == 0)
            {
                throw new CollectionEmptyException("Pizza");
            }
            return pizzas;
        }

        public async Task<Pizza> Update(Pizza entity)
        {
            var pizza = await Get(entity.Id);
            pizza.Name = entity.Name;
            pizza.Description = entity.Description;
            pizza.Price = entity.Price;
            pizza.Quantity = entity.Quantity;
            pizza.Image = entity.Image;
            return pizza;

        }
    }
}
