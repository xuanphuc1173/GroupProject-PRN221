using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppleStore.Pages.Admin.OrderDetailManage
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public EditModel(IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
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

            // Populate drop-down lists
            ViewData["OrderId"] = new SelectList(await _orderRepository.GetOrderAll(), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(await _productRepository.GetProductAll(), "ProductId", "ProductId");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

                await _orderDetailRepository.Update(OrderDetail);

            return RedirectToPage("./Index");
        }

        private async Task<bool> OrderDetailExists(int orderId, int productId)
        {
            return await _orderDetailRepository.GetOrderDetailByOrderIdProductId(orderId, productId) != null;
        }
    }
}
