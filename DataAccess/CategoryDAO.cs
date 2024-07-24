using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO: SingletonBase<CategoryDAO>
    {
        public async Task<IEnumerable<Category>> GetCategoryAll()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null) return null; return category;
        }
        public async Task Add(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Category category)
        {
            var existingItem = await GetCategoryById(category.CategoryId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(category);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var account = await GetCategoryById(id);
            if (account != null)
            {
                _context.Categories.Remove(account);
                await _context.SaveChangesAsync();
            }
        }
    }
}
