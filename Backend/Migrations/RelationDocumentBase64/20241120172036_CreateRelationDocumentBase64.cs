using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations.RelationDocumentBase64
{
    /// <inheritdoc />
    public partial class CreateRelationDocumentBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document_Base64",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Base64 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document_Base64");
        }
    }
}
