using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Repositories;
using BusinessObject;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppleStore.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace AppleStore.Pages.Admin.ProductManage
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<ProductHub> _hubContext;
        public CreateModel(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment, IHubContext<ProductHub> hubContext)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _categoryRepository.GetCategoryAll();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile ProductImage { get; set; } // Thêm thuộc tính cho ảnh

        public async Task<IActionResult> OnPostAsync()
        {


            if (ProductImage != null)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ProductImage.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProductImage.CopyToAsync(fileStream);
                }

                Product.ProductImage = "/uploads/" + uniqueFileName; // Cập nhật đường dẫn ảnh
            }

            await _productRepository.Add(Product);
            await _hubContext.Clients.All.SendAsync("ReceiveProductUpdate", Product);
            return RedirectToPage("./Index");
        }
    }
}
