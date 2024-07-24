using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderDetailRepository: IOrderDetailRepository
    {
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailAll()
        {
            return await OrderDetailDAO.Instance.GetOrderDetailAll();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(int id)
        {
            return await OrderDetailDAO.Instance.GetOrderDetailByOrderId(id);
        }        
        public async Task<OrderDetail> GetOrderDetailByOrderIdProductId(int OrderId, int ProductId)
        {
            return await OrderDetailDAO.Instance.GetOrderDetailByOrderIdProductId(OrderId, ProductId);
        }

        public async Task Add(OrderDetail orderDetail)
        {
            await OrderDetailDAO.Instance.Add(orderDetail);
        }

        public async Task Update(OrderDetail orderDetail)
        {
            await OrderDetailDAO.Instance.Update(orderDetail);
        }

        public async Task Delete(int OrderId,int ProductId)
        {
            await OrderDetailDAO.Instance.Delete(OrderId,ProductId);
        }
    }
}
