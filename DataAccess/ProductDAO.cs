using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO: SingletonBase<ProductDAO>
    {
        public async Task<IEnumerable<Product>> GetProductAll()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            if (product == null) return null; return product;
        }
        public async Task<IEnumerable<Product>> SearchProducts(string searchQuery, decimal? unitPrice)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.ProductName.Contains(searchQuery));
            }

            if (unitPrice.HasValue)
            {
                query = query.Where(p => p.UnitPrice <= unitPrice.Value);
            }

            return await query.ToListAsync();
        }
        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Product product)
        {
            var existingItem = await GetProductById(product.ProductId);
            if (existingItem != null)
            { 
                _context.Entry(existingItem).CurrentValues.SetValues(product);
            }

            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var product = await GetProductById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .SelectMany(o => o.OrderDetails)
                .SumAsync(od => od.Quantity * od.UnitPrice);
        }

        public async Task<List<Product>> GetTopSellingProductsAsync(int topN)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .SelectMany(o => o.OrderDetails)
                .GroupBy(od => od.Product)
                .Select(g => new
                {
                    Product = g.Key,
                    TotalRevenue = g.Sum(od => od.Quantity * od.UnitPrice)
                })
                .OrderByDescending(p => p.TotalRevenue)
                .Take(topN)
                .Select(p => p.Product)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<decimal> GetAverageProductPriceAsync()
        {
            return await _context.Products
                .AverageAsync(p => p.UnitPrice);
        }

        public async Task<List<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _context.Orders
                .Where(o => o.OrderDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<List<ProductOrderSummary>> GetOrderSummaryByProductAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .SelectMany(o => o.OrderDetails)
                .GroupBy(od => od.Product)
                .Select(g => new ProductOrderSummary
                {
                    ProductName = g.Key.ProductName,
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Quantity * od.UnitPrice)
                })
                .ToListAsync();
        }

        public async Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync(int year)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.OrderDate.Year == year)
                .SelectMany(o => o.OrderDetails)
                .GroupBy(od => od.Order.OrderDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(od => od.Quantity * od.UnitPrice)
                })
                .ToDictionaryAsync(p => p.Month, p => p.TotalRevenue);
        }
    }

    public class ProductOrderSummary
    {
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }

}
