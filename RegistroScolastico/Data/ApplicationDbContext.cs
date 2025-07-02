using Microsoft.EntityFrameworkCore;
using RegistroScolastico.Models;
using Microsoft.Extensions.Logging;

namespace RegistroScolastico.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        // Entità esistenti
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Profilo> Profili { get; set; }
        public DbSet<AnnoFormativo> AnniFormativi { get; set; }
        public DbSet<Corso> Corsi { get; set; }
        public DbSet<Classe> Classi { get; set; }
        public DbSet<CollegioDocenti> CollegiDocenti { get; set; }
        public DbSet<Docente> Docenti { get; set; }
        public DbSet<Studente> Studenti { get; set; }
        public DbSet<Materia> Materie { get; set; }
        public DbSet<Modulo> Moduli { get; set; }
        public DbSet<MateriaClasse> MaterieClassi { get; set; }
        public DbSet<ModuloMateria> ModuliMaterie { get; set; }
        public DbSet<ProvaMultidisciplinare> ProveMultidisciplinari { get; set; }
        public DbSet<ProvaMultidisciplinareSezione> ProveMultidisciplinariSezioni { get; set; }
        public DbSet<Colloquio> Colloqui { get; set; }
        public DbSet<ColloquioAmbito> ColloquiAmbiti { get; set; }
        public DbSet<ColloquioCriterio> ColloquiCriteri { get; set; }
        public DbSet<ValutazioneFinale> ValutazioniFinali { get; set; }
        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Sezione> Sezioni { get; set; }
        public DbSet<Anno> Anni { get; set; }

        // Nuove entità per la prova situazionale
        public DbSet<ProvaSituazionaleTemplate> ProveSituazionaliTemplate { get; set; }
        public DbSet<ProvaSituazionaleCompitoTemplate> ProveSituazionaliCompitiTemplate { get; set; }
        public DbSet<ProvaSituazionaleIndicatoreTemplate> ProveSituazionaliIndicatoriTemplate { get; set; }
        public DbSet<ProvaSituazionaleValutazione> ProveSituazionaliValutazioni { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _logger.LogInformation("Configuring database model");

            // Chiave primaria composta per ModuloMateria
            modelBuilder.Entity<ModuloMateria>()
                .HasKey(mm => new { mm.ModuloId, mm.MateriaId });

            modelBuilder.Entity<ModuloMateria>()
                .HasOne(mm => mm.Modulo)
                .WithMany(m => m.Materie)
                .HasForeignKey(mm => mm.ModuloId);

            modelBuilder.Entity<ModuloMateria>()
                .HasOne(mm => mm.Materia)
                .WithMany(m => m.Moduli)
                .HasForeignKey(mm => mm.MateriaId);

            // Relazione 1:1 Utente-Profilo
            modelBuilder.Entity<Utente>()
                .HasOne(u => u.Profilo)
                .WithOne(p => p.Utente)
                .HasForeignKey<Profilo>(p => p.UtenteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Classe - AnnoFormativo, Corso, CollegioDocenti
            modelBuilder.Entity<Classe>()
                .HasOne(c => c.AnnoFormativo)
                .WithMany(af => af.Classi)
                .HasForeignKey(c => c.AnnoFormativoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classe>()
                .HasOne(c => c.Corso)
                .WithMany(co => co.Classi)
                .HasForeignKey(c => c.CorsoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classe>()
                .HasOne(c => c.CollegioDocenti)
                .WithMany(cd => cd.Classi)
                .HasForeignKey(c => c.CollegioDocentiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Classe>()
                .HasIndex(c => new { c.AnnoId, c.SezioneId, c.AnnoFormativoId, c.CorsoId })
                .IsUnique();

            // Docente - CollegioDocenti (many-to-many)
            modelBuilder.Entity<Docente>()
                .HasMany(d => d.CollegiDocenti)
                .WithMany(cd => cd.Docenti)
                .UsingEntity<Dictionary<string, object>>(
                    "DocenteCollegio",
                    j => j.HasOne<CollegioDocenti>().WithMany().HasForeignKey("CollegioDocentiId"),
                    j => j.HasOne<Docente>().WithMany().HasForeignKey("DocenteId"));

            // Studente - Classe
            modelBuilder.Entity<Studente>()
                .HasOne(s => s.Classe)
                .WithMany(c => c.Studenti)
                .HasForeignKey(s => s.ClasseId)
                .OnDelete(DeleteBehavior.Restrict);

            // MateriaClasse (join con payload)
            modelBuilder.Entity<MateriaClasse>()
                .HasOne(mc => mc.Materia)
                .WithMany(m => m.Classi)
                .HasForeignKey(mc => mc.MateriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MateriaClasse>()
                .HasOne(mc => mc.Classe)
                .WithMany(c => c.MaterieClassi)
                .HasForeignKey(mc => mc.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MateriaClasse>()
                .HasOne(mc => mc.Docente)
                .WithMany(d => d.MaterieInsegnate)
                .HasForeignKey(mc => mc.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MateriaClasse>()
                .Property(mc => mc.Peso)
                .HasColumnType("decimal(18,2)");

            // --- NUOVE CONFIGURAZIONI PER PROVA SITUAZIONALE ---

            // ProvaSituazionaleTemplate - Classe (1:1)
            modelBuilder.Entity<ProvaSituazionaleTemplate>()
                .HasOne(t => t.Classe)
                .WithOne()
                .HasForeignKey<ProvaSituazionaleTemplate>(t => t.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProvaSituazionaleTemplate - Compiti (1:N)
            modelBuilder.Entity<ProvaSituazionaleTemplate>()
                .HasMany(t => t.Compiti)
                .WithOne(c => c.Template)
                .HasForeignKey(c => c.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProvaSituazionaleCompitoTemplate - Indicatori (1:N)
            modelBuilder.Entity<ProvaSituazionaleCompitoTemplate>()
                .HasMany(c => c.Indicatori)
                .WithOne(i => i.Compito)
                .HasForeignKey(i => i.CompitoId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProvaSituazionaleValutazione - Template (N:1)
            modelBuilder.Entity<ProvaSituazionaleValutazione>()
                .HasOne(v => v.Template)
                .WithMany()
                .HasForeignKey(v => v.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProvaSituazionaleValutazione - Studente (N:1)
            modelBuilder.Entity<ProvaSituazionaleValutazione>()
                .HasOne(v => v.Studente)
                .WithMany(s => s.ProveSituazionaliValutazioni)
                .HasForeignKey(v => v.StudenteId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProvaSituazionaleValutazione - Compito (N:1)
            modelBuilder.Entity<ProvaSituazionaleValutazione>()
                .HasOne(v => v.Compito)
                .WithMany()
                .HasForeignKey(v => v.CompitoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Chiave unica per evitare valutazioni duplicate
            modelBuilder.Entity<ProvaSituazionaleValutazione>()
                .HasIndex(v => new { v.StudenteId, v.CompitoId })
                .IsUnique();

            modelBuilder.Entity<ProvaSituazionaleValutazione>()
                .HasOne(v => v.Indicatore)
                .WithMany()
                .HasForeignKey(v => v.IndicatoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- FINE NUOVE CONFIGURAZIONI ---

            // Indici Unici
            modelBuilder.Entity<Utente>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Utente>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Profilo>()
                .HasIndex(p => p.CodiceFiscale)
                .IsUnique();

            // Valori di Default
            modelBuilder.Entity<Utente>()
                .Property(u => u.DataCreazione)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<AnnoFormativo>()
                .Property(af => af.DataInizio)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Materia>()
                .Property(m => m.DataCreazione)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Modulo>()
                .Property(mo => mo.DataCreazione)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<ProvaSituazionaleValutazione>()
                .Property(v => v.Livello)
                .HasDefaultValue(LivelloTecnico.Nullo);
        }
    }
}