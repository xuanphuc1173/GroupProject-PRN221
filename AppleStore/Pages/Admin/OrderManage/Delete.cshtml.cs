using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.OrderManage
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _orderRepository.GetOrderById(id.Value);

            if (Order == null)
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

            var Order = await _orderRepository.GetOrderById(id.Value);
            if (Order != null)
            {
                Order = Order;
                await _orderRepository.Delete(Order.OrderId);
            }

            return RedirectToPage("./Index");
        }
    }
}
