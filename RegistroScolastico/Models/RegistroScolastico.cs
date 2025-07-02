using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RegistroScolastico.Models
{
    // --- Enumerazioni ---
    public enum RuoloUtente
    {
        Amministratore,
        Docente,
        Studente
    }

    public enum LivelloTecnico
    {
        Nullo = 0,
        Parziale = 0,
        Base = 60,
        Intermedio = 80,
        Avanzato = 100
    }

    // --- Classi Base e di Profilo ---
    public class Utente
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public RuoloUtente Ruolo { get; set; }

        public Profilo Profilo { get; set; }

        public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataUltimoAccesso { get; set; }
        public bool Attivo { get; set; } = true;
    }

    public class Profilo
    {
        [Key, ForeignKey("Utente")]
        public int UtenteId { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

        [Required, MaxLength(50)]
        public string Cognome { get; set; }

        [MaxLength(16)]
        public string CodiceFiscale { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascita { get; set; }

        [MaxLength(200)]
        public string Indirizzo { get; set; }

        [Phone, MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(500)]
        public string? InfoAggiuntive { get; set; }

        public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataModifica { get; set; }

        public Utente Utente { get; set; }

        [NotMapped]
        public string NomeCompleto => $"{Nome} {Cognome}";
    }

    // --- Classi Strutturali della Scuola ---
    public class AnnoFormativo
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInizio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFine { get; set; }

        public ICollection<Classe> Classi { get; set; } = new List<Classe>();
    }

    public class Corso
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        public ICollection<Classe> Classi { get; set; } = new List<Classe>();
    }

    public class Classe
    {
        public int Id { get; set; }
        public int AnnoId { get; set; }
        public Anno Anno { get; set; }
        public int SezioneId { get; set; }
        public Sezione Sezione { get; set; }
        public int AnnoFormativoId { get; set; }
        public AnnoFormativo AnnoFormativo { get; set; }
        public int CorsoId { get; set; }
        public Corso Corso { get; set; }
        public int? CollegioDocentiId { get; set; }
        public CollegioDocenti? CollegioDocenti { get; set; }
        public ICollection<Studente> Studenti { get; set; } = new List<Studente>();
        public ICollection<MateriaClasse> MaterieClassi { get; set; } = new List<MateriaClasse>();
        public DateTime DataCreazione { get; set; }

        [NotMapped]
        public string NomeVisualizzato => $"{Anno?.Nome ?? ""} {Sezione?.Nome ?? ""}".Trim();
    }

    public class CollegioDocenti
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        public ICollection<Docente> Docenti { get; set; } = new List<Docente>();
        public ICollection<Classe> Classi { get; set; } = new List<Classe>();

        public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
    }

    // --- Ruoli Specifici ---
    public class Docente
    {
        [Key, ForeignKey("Utente")]
        public int UtenteId { get; set; }

        [Required]
        public Utente Utente { get; set; }

        public ICollection<CollegioDocenti> CollegiDocenti { get; set; } = new List<CollegioDocenti>();
        public ICollection<MateriaClasse> MaterieInsegnate { get; set; } = new List<MateriaClasse>();

        [NotMapped]
        public IEnumerable<Materia>? MaterieInsegnateDirette =>
            MaterieInsegnate?.Select(mc => mc.Materia).Distinct();
    }

    public class Studente
    {
        [Key, ForeignKey("Utente")]
        public int UtenteId { get; set; }

        [Required]
        public Utente Utente { get; set; }

        public int ClasseId { get; set; }
        public Classe Classe { get; set; }

        public ICollection<ProvaMultidisciplinare> ProveMultidisciplinari { get; set; } = new List<ProvaMultidisciplinare>();
        public ICollection<Colloquio> Colloqui { get; set; } = new List<Colloquio>();
        public ICollection<ValutazioneFinale> ValutazioniFinali { get; set; } = new List<ValutazioneFinale>();
        public ICollection<ProvaSituazionaleValutazione> ProveSituazionaliValutazioni { get; set; } = new List<ProvaSituazionaleValutazione>();
    }

    // --- Materie e Moduli ---
    public class Materia
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        public DateTime? DataCreazione { get; set; }
        public DateTime? DataModifica { get; set; }

        public ICollection<MateriaClasse> Classi { get; set; } = new List<MateriaClasse>();
        public ICollection<ModuloMateria> Moduli { get; set; } = new List<ModuloMateria>();

        [NotMapped]
        public IEnumerable<Docente>? DocentiDiretti { get; set; }
    }

    public class Modulo
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        public DateTime? DataCreazione { get; set; } = DateTime.UtcNow;
        public DateTime? DataModifica { get; set; }

        public ICollection<ModuloMateria> Materie { get; set; } = new List<ModuloMateria>();
    }

    public class ModuloMateria
    {
        public int ModuloId { get; set; }
        public Modulo Modulo { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }
    }

    // --- Tabelle di Join con payload ---
    public class MateriaClasse
    {
        public int Id { get; set; }
        public int? ClasseId { get; set; }
        public int MateriaId { get; set; }
        public int? DocenteId { get; set; }
        public Classe Classe { get; set; }
        public Materia Materia { get; set; }
        public Docente? Docente { get; set; }
        public decimal Peso { get; set; }
    }

    // --- Nuove entità per la gestione delle valutazioni ---

    // PROVA SITUAZIONALE
    public class ProvaSituazionaleTemplate
    {
        public int Id { get; set; }

        [Required]
        public int ClasseId { get; set; }
        public Classe Classe { get; set; }

        public List<ProvaSituazionaleCompitoTemplate> Compiti { get; set; }
            = new List<ProvaSituazionaleCompitoTemplate>();
    }

    public class ProvaSituazionaleCompitoTemplate
    {
        public int Id { get; set; }

        [Required]
        public int TemplateId { get; set; }
        public ProvaSituazionaleTemplate Template { get; set; }

        [Required, MaxLength(200)]
        public string Nome { get; set; }

        [Range(0, 1)]
        public double Peso { get; set; }

        public List<ProvaSituazionaleIndicatoreTemplate> Indicatori { get; set; }
            = new List<ProvaSituazionaleIndicatoreTemplate>();

        [NotMapped]
        public double PesoPercentuale
        {
            get => Peso * 100;
            set => Peso = value / 100;
        }

        [NotMapped]
        public double PunteggioMax => Indicatori.Sum(i => i.PunteggioMax);

        [NotMapped]
        public string NomeBreve => Nome.Length > 15 ? Nome.Substring(0, 12) + "..." : Nome;
    }

    public class ProvaSituazionaleIndicatoreTemplate
    {
        public int Id { get; set; }

        [Required]
        public int CompitoId { get; set; }
        public ProvaSituazionaleCompitoTemplate Compito { get; set; }

        [Required, MaxLength(200)]
        public string Nome { get; set; }

        [Range(1, 1000)]
        public double PunteggioMax { get; set; }
    }

    public class ProvaSituazionaleValutazione
    {
        public int Id { get; set; }

        [Required]
        public int TemplateId { get; set; }
        public ProvaSituazionaleTemplate Template { get; set; }

        [Required]
        public int StudenteId { get; set; }
        public Studente Studente { get; set; }

        [Required]
        public int CompitoId { get; set; }
        public ProvaSituazionaleCompitoTemplate Compito { get; set; }

        [Required]
        public int IndicatoreId { get; set; } // Aggiunto
        public ProvaSituazionaleIndicatoreTemplate Indicatore { get; set; } // Aggiunto

        [Required]
        public LivelloTecnico Livello { get; set; } = LivelloTecnico.Nullo;
    }

    // PROVA MULTIDISCIPLINARE
    public class ProvaMultidisciplinare
    {
        public int Id { get; set; }
        [Required]
        public int StudenteId { get; set; }
        public Studente Studente { get; set; }
        public ICollection<ProvaMultidisciplinareSezione> Sezioni { get; set; } = new List<ProvaMultidisciplinareSezione>();
    }

    public class ProvaMultidisciplinareSezione
    {
        public int Id { get; set; }
        [Required]
        public int ProvaMultidisciplinareId { get; set; }
        public ProvaMultidisciplinare ProvaMultidisciplinare { get; set; }
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        public string? Competenza { get; set; }
        [Range(0, 1)]
        public double Peso { get; set; }
        [Range(0, 100)]
        public int Punteggio { get; set; }
        [Range(1, 100)]
        public int NumDomande { get; set; }
        [Range(1, 100)]
        public int PuntiPerDomanda { get; set; }
    }

    // COLLOQUIO
    public class Colloquio
    {
        public int Id { get; set; }
        [Required]
        public int StudenteId { get; set; }
        public Studente Studente { get; set; }
        public ICollection<ColloquioAmbito> Ambiti { get; set; } = new List<ColloquioAmbito>();
    }

    public class ColloquioAmbito
    {
        public int Id { get; set; }
        [Required]
        public int ColloquioId { get; set; }
        public Colloquio Colloquio { get; set; }
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        public string? Competenza { get; set; }
        [Range(0, 1)]
        public double Peso { get; set; }
        public ICollection<ColloquioCriterio> Criteri { get; set; } = new List<ColloquioCriterio>();
    }

    public class ColloquioCriterio
    {
        public int Id { get; set; }
        [Required]
        public int AmbitoId { get; set; }
        public ColloquioAmbito Ambito { get; set; }
        [Required, MaxLength(100)]
        public string Descrizione { get; set; }
        [Range(1, 100)]
        public int PesoRelativo { get; set; }
        [Range(0, 10)]
        public int Voto { get; set; }
    }

    // VALUTAZIONE FINALE
    public class ValutazioneFinale
    {
        public int Id { get; set; }
        [Required]
        public int StudenteId { get; set; }
        public Studente Studente { get; set; }
        [Range(0, 100)]
        public int VotoAmmissione { get; set; }
        [Range(0, 100)]
        public int VotoFinale { get; set; }
        public DateTime DataCalcolo { get; set; } = DateTime.UtcNow;
        public int? MateriaId { get; set; }
        public Materia? Materia { get; set; }
    }

    // --- Eventi ---
    public class Evento
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Titolo { get; set; }
        [MaxLength(1000)]
        public string? Descrizione { get; set; }
        [Required]
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        [MaxLength(100)]
        public string? Luogo { get; set; }
        public int CreatoreId { get; set; }
        public Utente Creatore { get; set; }
    }

    public class Sezione
    {
        public int Id { get; set; }
        [Required, MaxLength(1)]
        public string Nome { get; set; }
    }

    public class Anno
    {
        public int Id { get; set; }
        [Required, MaxLength(5)]
        public string Nome { get; set; }
    }
}