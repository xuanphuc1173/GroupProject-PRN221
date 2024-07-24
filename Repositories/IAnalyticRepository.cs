using BusinessObject;

namespace Repositories
{
    public interface IAnalyticRepository
    {
        IEnumerable<Analytic> GetSoldProducts();
    }

}
