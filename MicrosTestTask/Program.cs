using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MicrosTestTask;
using MicrosTestTask.DAL;
using MicrosTestTask.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("LocalDb");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = new PathString("/Account/Login");
					options.AccessDeniedPath = new PathString("/Account/Login");
				});

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

builder.Services.AddRazorPages();
builder.Services.AddMvc();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<CheckUserStatusMiddleware>();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		  name: "default",
		  pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
