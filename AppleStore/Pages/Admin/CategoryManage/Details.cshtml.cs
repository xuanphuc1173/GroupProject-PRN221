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
    public class DetailsModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        public DetailsModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _categoryRepository.GetCategoryById(id.Value);
            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
