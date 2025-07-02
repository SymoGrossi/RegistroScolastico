using Microsoft.EntityFrameworkCore;
using RegistroScolastico.Interfacce;
using RegistroScolastico.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RegistroScolastico.Data;

public class ClasseService : IClasseService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ClasseService> _logger;

    public ClasseService(ApplicationDbContext context, ILogger<ClasseService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }

    public async Task<List<Classe>> GetClassiAsync()
    {
        _logger.LogInformation("Fetching all classi");
        return await _context.Classi.ToListAsync();
    }

    public async Task<Classe> GetClasseByIdAsync(int id)
    {
        _logger.LogInformation("Fetching classe by ID: {Id}", id);
        var classe = await _context.Classi.FindAsync(id);
        if (classe == null)
        {
            throw new InvalidOperationException($"Classe with ID {id} not found.");
        }
        return classe;
    }

    public async Task AddClasseAsync(Classe classe)
    {
        _context.Classi.Add(classe);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClasseAsync(Classe classe)
    {
        _context.Entry(classe).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClasseAsync(int id)
    {
        try
        {
            var classe = await _context.Classi.FindAsync(id);
            if (classe != null)
            {
                _context.Classi.Remove(classe);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting classe with ID: {Id}", id);
            throw;
        }
    }
}