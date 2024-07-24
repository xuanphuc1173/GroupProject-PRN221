using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using BusinessObject;
using Microsoft.AspNetCore.SignalR;
using AppleStore.Hubs;

namespace AppleStore.Pages.Areas.Admin.ProductManage
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IHubContext<ProductHub> _hubContext;

        public DeleteModel(IProductRepository productRepository, IHubContext<ProductHub> hubContext)
        {
            _productRepository = productRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _productRepository.GetProductById(id.Value);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductById(id.Value);
            if (product != null)
            {
                Product = product;
                await _productRepository.Delete(Product.ProductId);
                await _hubContext.Clients.All.SendAsync("ReceiveProductDeletion", Product.ProductId);
            }

            return RedirectToPage("./Index");
        }
    }
}
