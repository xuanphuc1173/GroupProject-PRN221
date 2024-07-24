using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppleStore.Pages.Carts
{
    public class CreateModel : PageModel
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CreateModel(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        [BindProperty]
        public CartItem CartItem { get; set; } = new CartItem();

        public SelectList Products { get; set; } = new SelectList(new List<Product>(), "ProductId", "ProductName");

        public async Task<IActionResult> OnGetAsync()
        {
            var products = await _productRepository.GetProductAll();
            Products = new SelectList(products, "ProductId", "ProductName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var products = await _productRepository.GetProductAll();
                Products = new SelectList(products, "ProductId", "ProductName");
                return Page();
            }

            // Kiểm tra và xử lý dữ liệu
            if (CartItem.ProductId == 0)
            {
                ModelState.AddModelError("", "Please select a valid product.");
                var products = await _productRepository.GetProductAll();
                Products = new SelectList(products, "ProductId", "ProductName");
                return Page();
            }

            await _cartRepository.AddCartItem(CartItem);
            return RedirectToPage("./Index");
        }

    }
}
