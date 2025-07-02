using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroScolastico.Migrations
{
    /// <inheritdoc />
    public partial class Creazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnniFormativi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnniFormativi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollegiDocenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegiDocenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Corsi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corsi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sezioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sezioni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruolo = table.Column<int>(type: "int", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataUltimoAccesso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuliMaterie",
                columns: table => new
                {
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuliMaterie", x => new { x.ModuloId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_ModuliMaterie_Materie_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuliMaterie_Moduli_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Moduli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnoId = table.Column<int>(type: "int", nullable: false),
                    SezioneId = table.Column<int>(type: "int", nullable: false),
                    AnnoFormativoId = table.Column<int>(type: "int", nullable: false),
                    CorsoId = table.Column<int>(type: "int", nullable: false),
                    CollegioDocentiId = table.Column<int>(type: "int", nullable: true),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classi_AnniFormativi_AnnoFormativoId",
                        column: x => x.AnnoFormativoId,
                        principalTable: "AnniFormativi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classi_Anni_AnnoId",
                        column: x => x.AnnoId,
                        principalTable: "Anni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classi_CollegiDocenti_CollegioDocentiId",
                        column: x => x.CollegioDocentiId,
                        principalTable: "CollegiDocenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classi_Corsi_CorsoId",
                        column: x => x.CorsoId,
                        principalTable: "Corsi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classi_Sezioni_SezioneId",
                        column: x => x.SezioneId,
                        principalTable: "Sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Docenti",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docenti", x => x.UtenteId);
                    table.ForeignKey(
                        name: "FK_Docenti_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Luogo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventi_Utenti_CreatoreId",
                        column: x => x.CreatoreId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profili",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    DataNascita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InfoAggiuntive = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profili", x => x.UtenteId);
                    table.ForeignKey(
                        name: "FK_Profili_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveSituazionaliTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClasseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveSituazionaliTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveSituazionaliTemplate_Classi_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.UtenteId);
                    table.ForeignKey(
                        name: "FK_Studenti_Classi_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studenti_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocenteCollegio",
                columns: table => new
                {
                    CollegioDocentiId = table.Column<int>(type: "int", nullable: false),
                    DocenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocenteCollegio", x => new { x.CollegioDocentiId, x.DocenteId });
                    table.ForeignKey(
                        name: "FK_DocenteCollegio_CollegiDocenti_CollegioDocentiId",
                        column: x => x.CollegioDocentiId,
                        principalTable: "CollegiDocenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocenteCollegio_Docenti_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Docenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterieClassi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClasseId = table.Column<int>(type: "int", nullable: true),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    DocenteId = table.Column<int>(type: "int", nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterieClassi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterieClassi_Classi_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterieClassi_Docenti_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Docenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterieClassi_Materie_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveSituazionaliCompitiTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveSituazionaliCompitiTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveSituazionaliCompitiTemplate_ProveSituazionaliTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "ProveSituazionaliTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colloqui",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colloqui", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colloqui_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveMultidisciplinari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveMultidisciplinari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveMultidisciplinari_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValutazioniFinali",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenteId = table.Column<int>(type: "int", nullable: false),
                    VotoAmmissione = table.Column<int>(type: "int", nullable: false),
                    VotoFinale = table.Column<int>(type: "int", nullable: false),
                    DataCalcolo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValutazioniFinali", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValutazioniFinali_Materie_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materie",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ValutazioniFinali_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveSituazionaliIndicatoriTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompitoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PunteggioMax = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveSituazionaliIndicatoriTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveSituazionaliIndicatoriTemplate_ProveSituazionaliCompitiTemplate_CompitoId",
                        column: x => x.CompitoId,
                        principalTable: "ProveSituazionaliCompitiTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColloquiAmbiti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColloquioId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Competenza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peso = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColloquiAmbiti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColloquiAmbiti_Colloqui_ColloquioId",
                        column: x => x.ColloquioId,
                        principalTable: "Colloqui",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveMultidisciplinariSezioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvaMultidisciplinareId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Competenza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    Punteggio = table.Column<int>(type: "int", nullable: false),
                    NumDomande = table.Column<int>(type: "int", nullable: false),
                    PuntiPerDomanda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveMultidisciplinariSezioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveMultidisciplinariSezioni_ProveMultidisciplinari_ProvaMultidisciplinareId",
                        column: x => x.ProvaMultidisciplinareId,
                        principalTable: "ProveMultidisciplinari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveSituazionaliValutazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    StudenteId = table.Column<int>(type: "int", nullable: false),
                    CompitoId = table.Column<int>(type: "int", nullable: false),
                    IndicatoreId = table.Column<int>(type: "int", nullable: false),
                    Livello = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveSituazionaliValutazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveSituazionaliValutazioni_ProveSituazionaliCompitiTemplate_CompitoId",
                        column: x => x.CompitoId,
                        principalTable: "ProveSituazionaliCompitiTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProveSituazionaliValutazioni_ProveSituazionaliIndicatoriTemplate_IndicatoreId",
                        column: x => x.IndicatoreId,
                        principalTable: "ProveSituazionaliIndicatoriTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProveSituazionaliValutazioni_ProveSituazionaliTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "ProveSituazionaliTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProveSituazionaliValutazioni_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColloquiCriteri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmbitoId = table.Column<int>(type: "int", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PesoRelativo = table.Column<int>(type: "int", nullable: false),
                    Voto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColloquiCriteri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColloquiCriteri_ColloquiAmbiti_AmbitoId",
                        column: x => x.AmbitoId,
                        principalTable: "ColloquiAmbiti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classi_AnnoFormativoId",
                table: "Classi",
                column: "AnnoFormativoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_AnnoId_SezioneId_AnnoFormativoId_CorsoId",
                table: "Classi",
                columns: new[] { "AnnoId", "SezioneId", "AnnoFormativoId", "CorsoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classi_CollegioDocentiId",
                table: "Classi",
                column: "CollegioDocentiId");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_CorsoId",
                table: "Classi",
                column: "CorsoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classi_SezioneId",
                table: "Classi",
                column: "SezioneId");

            migrationBuilder.CreateIndex(
                name: "IX_Colloqui_StudenteId",
                table: "Colloqui",
                column: "StudenteId");

            migrationBuilder.CreateIndex(
                name: "IX_ColloquiAmbiti_ColloquioId",
                table: "ColloquiAmbiti",
                column: "ColloquioId");

            migrationBuilder.CreateIndex(
                name: "IX_ColloquiCriteri_AmbitoId",
                table: "ColloquiCriteri",
                column: "AmbitoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteCollegio_DocenteId",
                table: "DocenteCollegio",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_CreatoreId",
                table: "Eventi",
                column: "CreatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterieClassi_ClasseId",
                table: "MaterieClassi",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterieClassi_DocenteId",
                table: "MaterieClassi",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterieClassi_MateriaId",
                table: "MaterieClassi",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuliMaterie_MateriaId",
                table: "ModuliMaterie",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Profili_CodiceFiscale",
                table: "Profili",
                column: "CodiceFiscale",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProveMultidisciplinari_StudenteId",
                table: "ProveMultidisciplinari",
                column: "StudenteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveMultidisciplinariSezioni_ProvaMultidisciplinareId",
                table: "ProveMultidisciplinariSezioni",
                column: "ProvaMultidisciplinareId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveSituazionaliCompitiTemplate_TemplateId",
                table: "ProveSituazionaliCompitiTemplate",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveSituazionaliIndicatoriTemplate_CompitoId",
                table: "ProveSituazionaliIndicatoriTemplate",
                column: "CompitoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveSituazionaliTemplate_ClasseId",
                table: "ProveSituazionaliTemplate",
                column: "ClasseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProveSituazionaliValutazioni_CompitoId",
                table: "ProveSituazionaliValutazioni",
                column: "CompitoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveSituazionaliValutazioni_IndicatoreId",
                table: "ProveSituazionaliValutazioni",
                column: "IndicatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveSituazionaliValutazioni_StudenteId_CompitoId",
                table: "ProveSituazionaliValutazioni",
                columns: new[] { "StudenteId", "CompitoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProveSituazionaliValutazioni_TemplateId",
                table: "ProveSituazionaliValutazioni",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_ClasseId",
                table: "Studenti",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Email",
                table: "Utenti",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Username",
                table: "Utenti",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValutazioniFinali_MateriaId",
                table: "ValutazioniFinali",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ValutazioniFinali_StudenteId",
                table: "ValutazioniFinali",
                column: "StudenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColloquiCriteri");

            migrationBuilder.DropTable(
                name: "DocenteCollegio");

            migrationBuilder.DropTable(
                name: "Eventi");

            migrationBuilder.DropTable(
                name: "MaterieClassi");

            migrationBuilder.DropTable(
                name: "ModuliMaterie");

            migrationBuilder.DropTable(
                name: "Profili");

            migrationBuilder.DropTable(
                name: "ProveMultidisciplinariSezioni");

            migrationBuilder.DropTable(
                name: "ProveSituazionaliValutazioni");

            migrationBuilder.DropTable(
                name: "ValutazioniFinali");

            migrationBuilder.DropTable(
                name: "ColloquiAmbiti");

            migrationBuilder.DropTable(
                name: "Docenti");

            migrationBuilder.DropTable(
                name: "Moduli");

            migrationBuilder.DropTable(
                name: "ProveMultidisciplinari");

            migrationBuilder.DropTable(
                name: "ProveSituazionaliIndicatoriTemplate");

            migrationBuilder.DropTable(
                name: "Materie");

            migrationBuilder.DropTable(
                name: "Colloqui");

            migrationBuilder.DropTable(
                name: "ProveSituazionaliCompitiTemplate");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "ProveSituazionaliTemplate");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Classi");

            migrationBuilder.DropTable(
                name: "AnniFormativi");

            migrationBuilder.DropTable(
                name: "Anni");

            migrationBuilder.DropTable(
                name: "CollegiDocenti");

            migrationBuilder.DropTable(
                name: "Corsi");

            migrationBuilder.DropTable(
                name: "Sezioni");
        }
    }
}
