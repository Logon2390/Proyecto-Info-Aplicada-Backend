using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations.Relation_User_Block_
{
    /// <inheritdoc />
    public partial class MakeRelationalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Block",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Block");
        }
    }
}
