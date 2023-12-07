using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class uniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email_CPF",
                table: "Patients",
                columns: new[] { "Email", "CPF" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Email_CPF_CRM",
                table: "Doctors",
                columns: new[] { "Email", "CPF", "CRM" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Email_CPF",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Email_CPF_CRM",
                table: "Doctors");
        }
    }
}
