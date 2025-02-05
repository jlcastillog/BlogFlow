using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogFlow.Core.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "User",
                newName: "Phone");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageProfile",
                table: "User",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "User",
                newName: "Token");
        }
    }
}
