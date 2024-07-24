using BusinessObject;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrderAll();
        Task<Order> GetOrderById(int id);
        Task Add(Order order);
        Task Update(Order order);
        Task Delete(int id);
    }
}