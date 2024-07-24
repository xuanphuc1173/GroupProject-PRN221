using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.Pages.Carts
{
    public class IndexModel : PageModel
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public IndexModel(ICartRepository cartRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public Cart Cart { get; set; } = new Cart();

        public async Task OnGetAsync()
        {
            var user = HttpContext.Session.GetString("AccountId");

            if (!string.IsNullOrEmpty(user))
            {
                Cart = await _cartRepository.GetCartByUserId(user);
            }
            else
            {
                RedirectToPage("/Login");
            }
        }

        public async Task<IActionResult> OnPostBuyAllAsync()
        {
            var userId = HttpContext.Session.GetString("AccountId");

            if (!string.IsNullOrEmpty(userId))
            {
                // Tạo đơn hàng mới
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(1),
                    ShippedDate = DateTime.Now.AddDays(7),
                    Freight = 15,
                    ShipAddress = "VN",
                    // Thêm thông tin khác của đơn hàng nếu cần
                };

                // Lưu đơn hàng vào cơ sở dữ liệu
                await _orderRepository.Add(order);

                // Thêm các mặt hàng từ giỏ hàng vào đơn hàng
                foreach (var item in Cart.Items)
                {
                    var cartItem = new CartItem
                    {
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,
                        CartId = item.Cart.CartId
                    };
                    await _cartRepository.AddCartItem(cartItem); // Thay vì OrderDetail, sử dụng CartItem
                }

                // Xóa tất cả các mặt hàng trong giỏ hàng
                await _cartRepository.ClearCartByUserId(userId);

                // Chuyển hướng đến trang Order hoặc trang khác để thông báo
                return RedirectToPage(); // Giả sử có trang Order để hiển thị đơn hàng
            }

            // Nếu không có người dùng hoặc có lỗi, quay lại trang hiện tại
            return RedirectToPage("/Login");
        }

    }
}
