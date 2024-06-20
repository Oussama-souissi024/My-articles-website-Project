using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using My_articles_website_Project.Code;
using My_articles_website_Project.Data;
using MyArticlesWebsiteProject.Core;
using MyArticlesWebsiteProject.Data.SqlServerEF;
using MyArticlesWebsiteProjectData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Register Identity services with role support
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>() // Add this line to support roles
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(op =>
{
	op.AddPolicy("User", p => p.RequireClaim("User", "User"));
	op.AddPolicy("Admin", p => p.RequireClaim("Admin", "Admin"));
});

// Inject Table
builder.Services.AddSingleton<IDataHelper<Category>, CategoryEnity>();
builder.Services.AddSingleton<IDataHelper<Author>, AuthorEnity>();
builder.Services.AddSingleton<IDataHelper<AuthorPost>, AuthorPostEntity>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	app.MapRazorPages();
});

//using (var scope = app.Services.CreateScope())
//{
//	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//	await SeedRolesAsync(roleManager);
//}

app.Run();

//async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
//{
//	var roles = new List<string> { "Admin", "User" };

//	foreach (var role in roles)
//	{
//		if (!await roleManager.RoleExistsAsync(role))
//		{
//			await roleManager.CreateAsync(new IdentityRole(role));
//		}
//	}
//}
