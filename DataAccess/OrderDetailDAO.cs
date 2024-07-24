using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO: SingletonBase<OrderDetailDAO>
    {
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailAll()
        {
            return await _context.OrderDetails.ToListAsync();
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(int Orderid)
        {
            var orders = await _context.OrderDetails.Where(c => c.OrderId == Orderid).ToListAsync();
            if (orders == null) return null; return orders;
        }
        public async Task<OrderDetail> GetOrderDetailByOrderIdProductId(int Orderid, int ProductId)
        {
            var order = await _context.OrderDetails.FirstOrDefaultAsync(c => c.OrderId == Orderid && c.ProductId == ProductId);
            if (order == null) return null; return order;
        }
        public async Task Add(OrderDetail order)
        {
            await _context.OrderDetails.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task Update(OrderDetail order)
        {
            var existingItem = await GetOrderDetailByOrderIdProductId(order.OrderId, order.ProductId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(order);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int Orderid,int ProductId)
        {
            var oder = await GetOrderDetailByOrderIdProductId(Orderid, ProductId);
            if (oder != null)
            {
                _context.OrderDetails.Remove(oder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
