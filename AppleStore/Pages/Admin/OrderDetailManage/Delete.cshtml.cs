using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.OrderDetailManage
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public DeleteModel(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? orderId, int? productId)
        {
            if (orderId == null || productId == null)
            {
                return NotFound();
            }

            OrderDetail = await _orderDetailRepository.GetOrderDetailByOrderIdProductId(orderId.Value, productId.Value);

            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (OrderDetail == null)
            {
                return NotFound();
            }

            await _orderDetailRepository.Delete(OrderDetail.OrderId, OrderDetail.ProductId);

            return RedirectToPage("./Index");
        }
    }
}
