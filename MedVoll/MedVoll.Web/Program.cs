using MedVoll.Web.Data;
using MedVoll.Web.Filters;
using MedVoll.Web.Interfaces;
using MedVoll.Web.Repositories;
using MedVoll.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExceptionHandlerFilter>();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionHandlerFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "VollMedAuthCookie"; // Nome do cookie
    //options.LoginPath = "/Account/Login"; // Redireciona para login se n�o autenticado
    //options.LogoutPath = "/Account/Logout"; // Caminho para logout
    options.AccessDeniedPath = "/Account/AccessDenied"; // Caminho para acesso negado
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5); // Tempo de expira��o
    options.SlidingExpiration = true; // Renova o cookie automaticamente

    options.Cookie.HttpOnly = true; // Impede acesso via JavaScript
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Exige HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict; // Restringe envio de cookies entre sites
});

builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
builder.Services.AddTransient<IMedicoService, MedicoService>();
builder.Services.AddTransient<IConsultaService, ConsultaService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true; // Exigir e-mails confirmados para login
    options.SignIn.RequireConfirmedPhoneNumber = false; // N�o exigir confirma��o de n�mero de telefone

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 3;

    options.Password.RequireDigit = true; // Exigir pelo menos um n�mero
    options.Password.RequireLowercase = true; // Exigir pelo menos uma letra min�scula
    options.Password.RequireUppercase = true; // Exigir pelo menos uma letra mai�scula
    options.Password.RequireNonAlphanumeric = true; // Exigir caracteres especiais
    options.Password.RequiredLength = 8; // Tamanho m�nimo da senha
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; // Proteger cookie contra acesso via JavaScript
    options.Cookie.IsEssential = true; // Garantir que o cookie seja salvo mesmo sem consentimento do usu�rio (GDPR)
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Exigir HTTPS para cookies
    options.IdleTimeout = TimeSpan.FromMinutes(1); // Tempo de expira��o da sess�o
});

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseSession();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithReExecute("/erro/{0}");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentitySeeder.SeedUsersAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao executar o Seeder: {ex.Message}");
    }
}

app.Run();
