using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.OrderManage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public CreateModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            await _orderRepository.Add(Order);

            return RedirectToPage("./Index");
        }
    }
}
