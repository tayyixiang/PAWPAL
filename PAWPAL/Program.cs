using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using PAWPAL.Components;
using PAWPAL.Data;
using PAWPAL.Components.Account;

var builder = WebApplication.CreateBuilder(args);

// Razor Components / Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();


builder.Services.AddScoped<PAWPAL.Services.ShelterService>();
builder.Services.AddScoped<PAWPAL.Services.AppointmentService>();
builder.Services.AddScoped<PAWPAL.Services.PetService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

// DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity + Roles
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

await TryMigrateAndSeedAsync(app);

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();

static async Task TryMigrateAndSeedAsync(WebApplication app)
{
    try
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        // Roles used in PawPal
        string[] roles = ["Admin", "Adopter"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = config["SeedAdmin:Email"];
        var adminPassword = config["SeedAdmin:Password"];

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            Console.WriteLine("SeedAdmin not configured. Skipping admin user seeding.");
            return;
        }

        var existing = await userManager.FindByEmailAsync(adminEmail);
        if (existing is null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (!createResult.Succeeded)
            {
                Console.WriteLine("Admin seed failed:");
                foreach (var e in createResult.Errors)
                    Console.WriteLine($" - {e.Code}: {e.Description}");
                return;
            }

            await userManager.AddToRoleAsync(adminUser, "Admin");
            Console.WriteLine($"Seeded Admin user: {adminEmail}");
        }
        else
        {
            if (!await userManager.IsInRoleAsync(existing, "Admin"))
                await userManager.AddToRoleAsync(existing, "Admin");

            Console.WriteLine($"Admin user exists: {adminEmail} (ensured role Admin)");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("WARNING: DB migrate/seed failed. The app will still start.");
        Console.WriteLine(ex.ToString());
    }
}