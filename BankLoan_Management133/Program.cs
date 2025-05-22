// C:\bankLoanmanagement133copy\BankLoan_Management133\BankLoan_Management133\Program.cs
using BankLoan_Management133.Models;
using BankLoan_Management133.Repository;
using BankLoan_Management133.BusinessLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BankLoan_Management133.BusinessLogicc.Implementation;
using BankLoan_Management133.BusinessLogicc.Interfaces;
using BankLoan_Management133.Repositoryy.Implementation;
using BankLoan_Management133.Repositoryy.Interfaces;
using BankLoan_Management133.Repositoryy.Models;
using BankLoan_Management133.BusinessLogicc;
using BankLoan_Management133.Repositoryy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("abc"))
);

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

// Register the repository and business logic
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBusinessLogic, CustomerBusinessLogic>();

builder.Services.AddScoped<ILoanProductRepository, LoanProductRepository>();
builder.Services.AddScoped<ILoanProductService, LoanProductService>();

builder.Services.AddScoped<ILoanApplicationRepository1, LoanApplicationRepository1>();
builder.Services.AddScoped<ILoanApplicationService1, LoanApplicationService1>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Enable authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();