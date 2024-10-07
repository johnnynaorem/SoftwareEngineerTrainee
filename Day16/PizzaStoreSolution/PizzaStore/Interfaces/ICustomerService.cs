using PizzaStore.Models;

namespace PizzaStore.Interfaces
{
    public interface ICustomerService
    {
        public Task<IEnumerable<Pizza>> ViewPizzas();
    }
}
