using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;
using BusinessObject;
using Microsoft.AspNetCore.SignalR;
using AppleStore.Hubs;

namespace AppleStore.Pages.Areas.Admin.ProductManage
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHubContext<ProductHub> _hubContext;

        public EditModel(IProductRepository productRepository, ICategoryRepository categoryRepository, IHubContext<ProductHub> hubContext)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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

            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetCategoryAll(), "CategoryId", "CategoryName", Product.CategoryId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {



                await _productRepository.Update(Product);

            await _hubContext.Clients.All.SendAsync("ReceiveProductUpdate", Product);
            return RedirectToPage("./Index");
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _productRepository.GetProductById(id) != null;
        }
    }
}
