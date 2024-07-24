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

namespace AppleStore.Pages.Admin.CategoryManage
{
    public class EditModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public EditModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

                await _categoryRepository.Update(Category);


            return RedirectToPage("./Index");
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _categoryRepository.GetCategoryById(id) != null;
        }
    }
}
