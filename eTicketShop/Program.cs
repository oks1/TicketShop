using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eTicketShop.Areas.Identity.Data;
using eTicketShop.Core;
using eTicketShop.Core.Repositories;
using eTicketShop.Repositories;
using eTicketShop.Interface;
using eTicketShop.Data.Cart;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TicketShopDB2ContextConnection") ?? throw new InvalidOperationException("Connection string 'TicketShopDB2ContextConnection' not found.");

builder.Services.AddDbContext<TicketShopDB2Context>(options =>
    options.UseSqlServer(connectionString));

AddScoped();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TicketShopDB2Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();



#region Authorization

AddAuthorizationPolicies();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();;

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "events",
//    pattern: "/events/{categorySlug?}",
//        defaults: new { controller = "Events", action = "Index" }
//  );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Events}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
    });
}

void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IEvent, EventService> ();
    builder.Services.AddScoped<IOrder, OrdersService>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
    builder.Services.AddMemoryCache();
    builder.Services.AddSession();
    builder.Services.AddControllersWithViews();
}