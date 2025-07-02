using RegistroScolastico.Components.Pages;
using RegistroScolastico.Models;
using Microsoft.Extensions.Logging;

namespace RegistroScolastico.Interfacce
{
    public interface IClasseService
    {
        Task<List<Classe>> GetClassiAsync();
        Task<Classe> GetClasseByIdAsync(int id);
        Task AddClasseAsync(Classe classe);
        Task UpdateClasseAsync(Classe classe);
        Task DeleteClasseAsync(int id);
    }
}