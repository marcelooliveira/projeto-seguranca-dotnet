using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedVoll.Web.Migrations
{
    public partial class SeedMedicosConsultas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Populando a tabela Medicos
            migrationBuilder.InsertData(
                table: "Medicos",
                columns: new[] { "Nome", "Email", "Telefone", "Crm", "Especialidade" },
                columnTypes: new[] { "string", "string", "string", "string", "string" }, 
                values: new object[,]
                {
                { "Gregory House", "house@hospital.com", "(12)12345-6781", "123456", "ORTOPEDIA" },
                { "Meredith Grey", "meredith@hospital.com", "(12)12345-6782", "654321", "CARDIOLOGIA" },
                { "John Carter", "carter@hospital.com", "(12)12345-6783", "234567", "GINECOLOGIA" },
                    // Adicione mais 17 registros fictícios
                });

            // Populando a tabela Consultas com associações fictícias com a tabela Medicos
            migrationBuilder.InsertData(
                table: "Consultas",
                columns: new[] { "MedicoId", "Paciente", "Data" },
                columnTypes: new[] { "int", "string", "DateTime" },
                values: new object[,]
                {
                { 1, "John Doe", new DateTime(2024, 11, 10, 10, 0, 0) },
                { 1, "Jane Doe", new DateTime(2024, 11, 11, 12, 0, 0) },
                { 2, "Alex Karev", new DateTime(2024, 11, 12, 9, 0, 0) },
                    // Adicione mais 17 registros fictícios
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Consultas");
            migrationBuilder.Sql("DELETE FROM Medicos");
        }
    }
}
