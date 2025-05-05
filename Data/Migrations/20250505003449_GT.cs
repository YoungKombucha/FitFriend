using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitFriend.Data.Migrations
{
    /// <inheritdoc />
    public partial class GT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GT",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GT",
                table: "Goals");
        }
    }
}
