using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.OrderDetailManage
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public CreateModel(IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Populate dropdown lists
            ViewData["OrderId"] = new SelectList(await _orderRepository.GetOrderAll(), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(await _productRepository.GetProductAll(), "ProductId", "ProductId");
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {

            await _orderDetailRepository.Add(OrderDetail);
            return RedirectToPage("./Index");
        }
    }
}
