using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Carts
{
    public class DeleteModel : PageModel
    {
        private readonly ICartRepository _cartRepository;

        public DeleteModel(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [BindProperty]
        public int CartItemId { get; set; }
        public async Task<IActionResult> OnGetAsync(int cartItemId)
        {
            // Lưu CartItemId để sử dụng trong form
            CartItemId = cartItemId;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Kiểm tra tính hợp lệ của CartItemId
            if (CartItemId <= 0)
            {
                // Trả về trang giỏ hàng nếu CartItemId không hợp lệ
                return RedirectToPage("/Carts/Index");
            }

            // Gọi phương thức xóa trong repository
            var result = await _cartRepository.DeleteCartItem(CartItemId);

            if (result)
            {
                // Chuyển hướng về trang giỏ hàng nếu xóa thành công
                return RedirectToPage("/Carts/Index");
            }

            // Nếu xóa không thành công, bạn có thể thêm thông báo lỗi ở đây
            return RedirectToPage("/Carts/Index");
        }
    }
}
