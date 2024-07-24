using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
namespace Repositories
{
    public class OrderRepository:IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetOrderAll()
        {
            return await OrderDAO.Instance.GetOrderAll();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await OrderDAO.Instance.GetOrderById(id);
        }

        public async Task Add(Order order)
        {
            await OrderDAO.Instance.Add(order);
        }

        public async Task Update(Order order)
        {
            await OrderDAO.Instance.Update(order);
        }

        public async Task Delete(int id)
        {
            await OrderDAO.Instance.Delete(id);
        }
    }
}