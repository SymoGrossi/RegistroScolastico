using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RegistroScolastico.Components;
using RegistroScolastico.Data;
using RegistroScolastico.Interfacce;

var builder = WebApplication.CreateBuilder(args);

// Registrazione servizi
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrazione della factory per i componenti che la richiedono
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrazione servizi custom
builder.Services.AddScoped<IClasseService, ClasseService>();

var app = builder.Build();

// Verifica risoluzione DbContextFactory
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var factory = services.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
        using var context = factory.CreateDbContext();
        Console.WriteLine("DbContextFactory e DbContext risolti correttamente");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Errore durante la risoluzione del DbContextFactory: {ex.Message}");
    }
}

// Configurazione pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Il valore HSTS di default è 30 giorni. Modifica per la produzione se necessario.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();