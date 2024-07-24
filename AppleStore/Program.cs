using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Microsoft.AspNetCore.SignalR;
using AppleStore.Hubs;
using DataAccess;
var builder = WebApplication.CreateBuilder(args);

// Configure logging to write to the console
builder.Logging.ClearProviders(); // Remove other logging providers
builder.Logging.AddConsole(); // Add console logger

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add DbContext with configuration from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AnalyticDAO with dependency injection
builder.Services.AddScoped<AnalyticDAO>(provider =>
{
    var context = provider.GetRequiredService<ApplicationDbContext>();
    var analyticDAO = new AnalyticDAO();
    analyticDAO.SetContext(context);
    return analyticDAO;
});
builder.Services.AddSignalR();

builder.Services.AddRazorPages();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthorization();
app.UseAuthentication();

app.MapHub<ProductHub>("/productHub");

app.MapRazorPages();

app.Run();
