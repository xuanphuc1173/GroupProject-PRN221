using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;

namespace AppleStore.Pages.Admin.CategoryManage
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly ICategoryRepository _categoryRepository;
        public IndexModel(ILogger<Index> logger, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        public IList<Category> Category { get;set; } = new List<Category>();

        public async Task OnGetAsync()
        {
            Category =  (await _categoryRepository.GetCategoryAll()).ToList();
        }
    }
}
