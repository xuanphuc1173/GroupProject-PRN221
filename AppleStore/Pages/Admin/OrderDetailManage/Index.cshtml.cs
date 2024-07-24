using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.OrderDetailManage
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public IndexModel(ILogger<Index> logger, IOrderDetailRepository orderDetailRepository)
        {
            _logger = logger;
            _orderDetailRepository = orderDetailRepository;
        }

        public IList<OrderDetail> OrderDetail { get;set; } = new List<OrderDetail>();

        public async Task OnGetAsync()
        {
            try
            {
                OrderDetail = (await _orderDetailRepository.GetOrderDetailAll()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                OrderDetail = new List<OrderDetail>(); 
            }

        }
    }
}
