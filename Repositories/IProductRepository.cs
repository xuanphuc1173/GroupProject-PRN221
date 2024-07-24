using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductAll();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> SearchProducts(string searchQuery, decimal? unitPrice);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }
}
