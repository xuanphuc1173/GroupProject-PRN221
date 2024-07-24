using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AnalyticDAO
    {
        private ApplicationDbContext _context;

        public void SetContext(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Analytic>> GetSoldProductsAsync()
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Context not set.");
            }

            var analytics = await _context.OrderDetails
                .GroupBy(od => new { od.Product.ProductName })
                .Select(g => new Analytic
                {
                    ProductName = g.Key.ProductName,
                    QuantitySold = g.Sum(od => od.Quantity),
                    TotalAmountSold = g.Sum(od => od.Quantity * od.UnitPrice)
                })
                .ToListAsync();

            return analytics;
        }
    }
}
