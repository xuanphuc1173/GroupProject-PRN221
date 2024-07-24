using BusinessObject;

namespace Repositories
{
    public class AnalyticRepository : IAnalyticRepository
    {
        private readonly ApplicationDbContext _context;

        public AnalyticRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Analytic> GetSoldProducts()
        {
            var analytics = from o in _context.Orders
                            join od in _context.OrderDetails on o.OrderId equals od.OrderId
                            join p in _context.Products on od.ProductId equals p.ProductId
                            group new { od.Quantity, od.UnitPrice } by new { p.ProductId, p.ProductName } into g
                            select new Analytic
                            {
                                ProductId = g.Key.ProductId,
                                ProductName = g.Key.ProductName,
                                QuantitySold = g.Sum(x => x.Quantity),
                                TotalAmountSold = g.Sum(x => x.Quantity * x.UnitPrice)
                            };

            return analytics.ToList();
        }
    }

}
