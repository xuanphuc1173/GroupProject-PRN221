using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repositories;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AppleStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository, ICartRepository cartRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public IEnumerable<Product> ProductList { get; set; } = Enumerable.Empty<Product>();
        [TempData]
        public string SuccessMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? UnitPrice { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchQuery) || UnitPrice.HasValue)
                {
                    ProductList = await _productRepository.SearchProducts(SearchQuery, UnitPrice);
                }
                else
                {
                    ProductList = await _productRepository.GetProductAll();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                ProductList = Enumerable.Empty<Product>();
            }
        }
        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            var user = HttpContext.Session.GetString("AccountId");

            if (!string.IsNullOrEmpty(user))
            {
                var cart = await _cartRepository.GetCartByUserId(user);

                if (cart == null)
                {
                    cart = new Cart { UserId = user };
                    await _cartRepository.AddCart(cart);
                }

                var existingCartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
                if (existingCartItem != null)
                {
                    // Nếu sản phẩm đã có, chỉ cần cập nhật số lượng
                    existingCartItem.Quantity += 1;
                    await _cartRepository.UpdateCartItem(existingCartItem);
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        ProductId = productId,
                        Quantity = 1,
                        CartId = cart.CartId
                    };
                    await _cartRepository.AddCartItem(cartItem);
                }
                // Giảm số lượng sản phẩm trong kho
                var product = await _productRepository.GetProductById(productId);
                if (product != null)
                {
                    //product.Quantity -= 1;
                    //await _productRepository.UpdateProduct(product);

                    if (int.TryParse(product.QuantityPerUnit, out int quantity))
                    {
                        quantity -= 1; // Giảm số lượng
                        product.QuantityPerUnit = quantity.ToString();
                        await _productRepository.Update(product);
                    }
                }

                SuccessMessage = "Product added to cart successfully!";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }




    }
}
