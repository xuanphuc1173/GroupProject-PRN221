using System.Collections.Generic;
using System.Linq; // Thêm using System.Linq
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repositories;
using BusinessObject;

namespace AppleStore.Pages
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly IProductRepository _productRepository;

        public Index(ILogger<Index> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IList<Product> ProductList { get; set; } = new List<Product>();
        public string SuccessMessage { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            try
            {
                ProductList = (await _productRepository.GetProductAll()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                ProductList = new List<Product>(); // Return an empty list or handle the error accordingly
            }
        }
    }
}
