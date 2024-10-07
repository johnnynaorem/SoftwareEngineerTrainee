using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;
using PizzaStoreAPI.Repositories;

namespace PizzaStoreAPI.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, Pizza> _pizzaRepository;
        private readonly IRepository<int, Cart> _cartRepository;

        public CartService(
            IRepository<int, Customer> customerRepository,
            IRepository<int, Pizza> pizzaRepository,
            IRepository<int, Cart> cartRepository
        )
        {
            _customerRepository = customerRepository;
            _pizzaRepository = pizzaRepository;
            _cartRepository = cartRepository;
        }

        public async Task<PizzaCartDTO> AddPizzaToCart(PizzaCartDTO pizzaCartDTO, int customerId)
        {
            if (!(await DoesCustomerHaveCart(customerId)))
            {
                var cart = await CreateNewCart(customerId, pizzaCartDTO.PizzaId, pizzaCartDTO.Quantity);
                //await _cartRepository.Add(cart);
                return pizzaCartDTO;
            }

            var custCart = await GetCustomerCart(customerId);
            if (await DoesCartContainPizza(custCart.CartNumber, pizzaCartDTO.PizzaId))
            {
                var pizza = custCart.Pizzas.FirstOrDefault(p => p.Id == pizzaCartDTO.PizzaId);
                pizza.Quantity += pizzaCartDTO.Quantity;
            }
            else
            {
                custCart.Pizzas.Add(await CreatePizza(pizzaCartDTO));
            }

            await _cartRepository.Update(custCart);
            return pizzaCartDTO;
        }

        
        async Task<Pizza> CreatePizza(PizzaCartDTO pizzaCartDTO)
        {
            var pizzaFromRepo = await _pizzaRepository.Get(pizzaCartDTO.PizzaId);
            if (pizzaFromRepo == null)
            {
                throw new NoEntityFoundException("Pizza", pizzaCartDTO.PizzaId);
            }

            
            return new Pizza()
            {
                Id = pizzaFromRepo.Id,
                Name = pizzaFromRepo.Name,
                Price = pizzaFromRepo.Price,
                Quantity = pizzaCartDTO.Quantity
            };
        }

        
        async Task<bool> DoesCartContainPizza(int cartId, int pizzaId)
        {
            var cart = await _cartRepository.Get(cartId);
            return cart.Pizzas.Any(p => p.Id == pizzaId);
        }

        
        async Task<Cart> GetCustomerCart(int customerId)
        {
            var carts = await _cartRepository.GetAll();
            return carts.FirstOrDefault(c => c.CustomerId == customerId);
        }

        
        async Task<bool> DoesCustomerHaveCart(int customerId)
        {
            try
            {
                var customer = await _customerRepository.Get(customerId);
                var carts = await _cartRepository.GetAll();
                return carts.Any(c => c.CustomerId == customerId);
            }
            catch (CollectionEmptyException)
            {
                return false;
            }
        }

        
        async Task<Cart> CreateNewCart(int customerId, int pizzaId, int qty)
        {
            var pizza = await _pizzaRepository.Get(pizzaId);
            if (pizza == null)
            {
                throw new NoEntityFoundException("Pizza", pizzaId);
            }

            Cart cart = new Cart()
            {
                CustomerId = customerId,
                Pizzas = new List<Pizza>
                {
                    new Pizza()
                    {
                        Id = pizza.Id,
                        Name = pizza.Name,
                        Price = pizza.Price,
                        Quantity = qty
                    }
                }
            };
            return await _cartRepository.Add(cart);
        }

        public async Task<CartDTO> GetCart(int customerId)
        {
            var cart = await GetCustomerCart(customerId);
            if (cart == null)
            {
                throw new NoEntityFoundException("Cart", customerId);
            }

            var pizzaDtoObjects = await MapPizzaToPizzaDTO(cart.Pizzas);
            return new CartDTO()
            {
                CartNumber = cart.CartNumber,
                Pizzas = pizzaDtoObjects
            };
        }

        async Task<List<PizzaCartDTO>> MapPizzaToPizzaDTO(List<Pizza> pizzas)
        {
            return pizzas.Select(p => new PizzaCartDTO
            {
                PizzaId = p.Id,
                PizzaName = p.Name,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();
        }

        public Task<CartDTO> UpdateCart(int cartNumber, PizzaCartDTO pizzaCartDTO)
        {
            throw new NotImplementedException();
        }
    }
}
