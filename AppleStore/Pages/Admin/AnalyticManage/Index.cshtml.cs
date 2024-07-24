using BusinessObject;
using DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppleStore.Pages.Admin.AnalyticManage
{
    public class IndexModel : PageModel
    {
        private readonly AnalyticDAO _analyticDAO;

        public IndexModel(AnalyticDAO analyticDAO)
        {
            _analyticDAO = analyticDAO;
        }

        public IEnumerable<Analytic> Analytics { get; set; } // Ensure this property is defined

        public async Task OnGetAsync()
        {
            Analytics = await _analyticDAO.GetSoldProductsAsync();
        }
    }
}
