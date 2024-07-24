using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;
using BusinessObject;
using System.Threading.Tasks;
using System.Linq;

namespace AppleStore.Pages.Carts
{
    public class EditModel : PageModel
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public EditModel(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        [BindProperty]
        public CartItem CartItem { get; set; } = default!;

        public SelectList Products { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? cartItemId)
        {
            if (cartItemId == null)
            {
                return NotFound();
            }

            // Lấy thông tin CartItem từ cơ sở dữ liệu
            CartItem = await _cartRepository.GetCartItemById(cartItemId.Value);
            if (CartItem == null)
            {
                return NotFound();
            }

            // Lấy danh sách sản phẩm cho dropdown
            var products = await _productRepository.GetProductAll();
            Products = new SelectList(products, "ProductId", "ProductName");

            // Lấy thông tin sản phẩm để hiển thị
            var product = products.FirstOrDefault(p => p.ProductId == CartItem.ProductId);
            if (product != null)
            {
                ProductName = product.ProductName;
                Price = product.UnitPrice;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _cartRepository.UpdateCartItem(CartItem);

            return RedirectToPage("./Index");
        }
    }
}
