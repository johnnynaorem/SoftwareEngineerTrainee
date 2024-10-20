using EFCoreWebAPI.Context;
using EFCoreWebAPI.Exceptions;
using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWebAPI.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        private readonly ShoppingContext _context;

        public ProductRepository(ShoppingContext shoppingContext)
        {
            _context = shoppingContext;
        }
        public async Task<Product> Add(Product entity)
        {
            try
            {
                await _context.Products.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Product");
            }
        }

        public async Task<Product> Delete(int key)
        {
            var product = await Get(key);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
            throw new NotFoundException("Product. Product Delete Fail");
        }

        public async Task<Product> Get(int key)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == key);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Any())
            {
                return products;
            }
            throw new CollectionEmptyException("Products");
        }

        public async Task<Product> Update(Product entity, int pid)
        {
            var oldProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == pid);
            if (oldProduct != null)
            {
                oldProduct.Price = entity.Price;
                await _context.SaveChangesAsync();
                return oldProduct;
            }
            throw new NotUpdateException("Product Price Update Fail");
        }
    }
}
