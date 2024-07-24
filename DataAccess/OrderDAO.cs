using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO : SingletonBase<OrderDAO>
    {
        public async Task<IEnumerable<Order>> GetOrderAll()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == id);
            if (order == null) return null; return order;
        }
        public async Task Add(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Order order)
        {
            var existingItem = await GetOrderById(order.OrderId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(order);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var oder = await GetOrderById(id);
            if (oder != null) 
            {
             _context.Orders.Remove(oder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
