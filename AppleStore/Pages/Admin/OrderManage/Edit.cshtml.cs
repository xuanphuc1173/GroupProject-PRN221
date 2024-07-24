using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.OrderManage
{
    public class EditModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public EditModel(IOrderRepository orderRepository)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

                await _orderRepository.Update(Order);

            return RedirectToPage("./Index");
        }

        private async Task<bool> OrderExists(int id)
        {
            return await _orderRepository.GetOrderById(id) != null;
        }
    }
}
