using PizzaStore.Interfaces;
using PizzaStore.Models;

namespace PizzaStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Pizza> _pizzaRepository;
        public CustomerService(IRepository<int, Pizza> pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task<IEnumerable<Pizza>> ViewPizzas()
        {
            var pizzas = (await _pizzaRepository.GetAll()).ToList().OrderBy(p=>p.Price);
            return pizzas;
        }
    }
}
