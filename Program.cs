using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelpDeskWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC + Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Identity
builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


// CRIAR ROLES E USUÁRIOS
using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Roles do sistema
    string[] roles =
    {
        "Administrador",
        "Tecnico",
        "Usuario"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(
                new IdentityRole(role)
            );
        }
    }

    // USUÁRIO COMUM
    var emailUsuario = "usuario@helpdesk.com";

    var usuarioComum =
        await userManager.FindByEmailAsync(emailUsuario);

    if (usuarioComum != null)
    {
        if (!await userManager.IsInRoleAsync(
                usuarioComum,
                "Usuario"))
        {
            await userManager.AddToRoleAsync(
                usuarioComum,
                "Usuario"
            );
        }
    }

    // ADMINISTRADOR
    // Credenciais carregadas por variáveis de ambiente
    var emailAdmin =
        Environment.GetEnvironmentVariable("HELPDESK_ADMIN_EMAIL");

    var senhaAdmin =
        Environment.GetEnvironmentVariable("HELPDESK_ADMIN_PASSWORD");

    if (!string.IsNullOrWhiteSpace(emailAdmin) &&
        !string.IsNullOrWhiteSpace(senhaAdmin))
    {
        var admin =
            await userManager.FindByEmailAsync(emailAdmin);

        if (admin == null)
        {
            admin = new IdentityUser
            {
                UserName = emailAdmin,
                Email = emailAdmin,
                EmailConfirmed = true
            };

            var resultado =
                await userManager.CreateAsync(
                    admin,
                    senhaAdmin
                );

            if (resultado.Succeeded)
            {
                await userManager.AddToRoleAsync(
                    admin,
                    "Administrador"
                );
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(
                    admin,
                    "Administrador"))
            {
                await userManager.AddToRoleAsync(
                    admin,
                    "Administrador"
                );
            }
        }
    }

    // TÉCNICO
    // Credenciais carregadas por variáveis de ambiente
    var emailTecnico =
        Environment.GetEnvironmentVariable("HELPDESK_TECNICO_EMAIL");

    var senhaTecnico =
        Environment.GetEnvironmentVariable("HELPDESK_TECNICO_PASSWORD");

    if (!string.IsNullOrWhiteSpace(emailTecnico) &&
        !string.IsNullOrWhiteSpace(senhaTecnico))
    {
        var tecnico =
            await userManager.FindByEmailAsync(emailTecnico);

        if (tecnico == null)
        {
            tecnico = new IdentityUser
            {
                UserName = emailTecnico,
                Email = emailTecnico,
                EmailConfirmed = true
            };

            var resultadoTecnico =
                await userManager.CreateAsync(
                    tecnico,
                    senhaTecnico
                );

            if (resultadoTecnico.Succeeded)
            {
                await userManager.AddToRoleAsync(
                    tecnico,
                    "Tecnico"
                );
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(
                    tecnico,
                    "Tecnico"))
            {
                await userManager.AddToRoleAsync(
                    tecnico,
                    "Tecnico"
                );
            }
        }
    }
}

app.Run();
