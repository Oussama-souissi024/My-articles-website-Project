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

//builder.Services.AddDbContext<MyDBContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(op =>
{
	op.AddPolicy("User", p => p.RequireClaim("User", "User"));
	op.AddPolicy("SuperUser", p => p.RequireClaim("SuperUser", "SuperUser"));
	op.AddPolicy("Admin", p => p.RequireClaim("Admin", "Admin"));
}
); 

//Inject Table
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

//app.UseMvcWithDefaultRoute();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

	app.MapRazorPages();
});


app.Run();
